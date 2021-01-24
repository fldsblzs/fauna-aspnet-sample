using AutoMapper;
using FaunaAspNet.API.Messages;
using FaunaAspNet.API.Models;
using FaunaDB.Client;
using FaunaDB.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FaunaDB.Query.Language;

namespace FaunaAspNet.API.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private const string Collection = "artist";
        private const string ArtistNameIndex = "name-index";

        private readonly FaunaClient _faunaClient;
        private readonly IMapper _mapper;

        public ArtistRepository(FaunaClient faunaClient, IMapper mapper)
        {
            _faunaClient = faunaClient;
            _mapper = mapper;
        }

        public async Task<Artist> GetArtistAsync(string id)
        {
            var value = await _faunaClient
                .Query(Get(Ref(Collection(Collection), id)));

            return DecodeArtist(value);
        }

        public async Task<Artist> GetArtistByNameAsync(string name)
        {
            var value = await _faunaClient
                .Query(Get(Match(Index(ArtistNameIndex), name)));

            return DecodeArtist(value);
        }

        // TODO: Implement pagination
        public async Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            var value = await _faunaClient
                .Query(Map(Paginate(Documents(Collection(Collection))), Lambda(x => Get(x)))
                );

            var arrayV = value.At("data").To<ArrayV>().Value;
            var artists = new List<Artist>(arrayV.Length);
            artists.AddRange(arrayV.Select(DecodeArtist));

            return artists;
        }

        public async Task<Artist> CreateOrUpdateArtist(ArtistMessage artistMessage, string id = default)
        {
            var artistFromUser = _mapper.Map<Artist>(artistMessage);

            var value = id == default
                ? await _faunaClient.Query(
                    Create(
                        Collection(Collection),
                        Obj("data", Encoder.Encode(artistFromUser))))
                : await _faunaClient.Query(
                    Replace(
                        Ref(Collection(Collection), id),
                        Obj("data", Encoder.Encode(artistFromUser))));

            return DecodeArtist(value);
        }

        public async Task DeleteArtistAsync(string id)
        {
            await _faunaClient
                .Query(Delete(Ref(Collection(Collection), id)));
        }

        private Artist DecodeArtist(Value payload)
        {
            Artist artist = default;

            payload.At("data")
                .To<Artist>()
                .Match(d => artist = d, reason => throw new Exception(reason));

            payload.At("ref")
                .To<RefV>()
                .Match(r => artist.Id = r.Value.Id, reason => throw new Exception(reason));

            return artist;
        }
    }
}