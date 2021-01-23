using AutoMapper;
using FaunaAspNet.API.Messages;
using FaunaAspNet.API.Models;
using FaunaDB.Client;
using FaunaDB.Types;
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
            var value = await _faunaClient.Query(
                Get(Ref(Collection(Collection), id)));

            return DecodeArtist(value);
        }

        public async Task<Artist> GetArtistByNameAsync(string name)
        {
            var value = await _faunaClient.Query(
                Get(
                    Match(Index(ArtistNameIndex), name)));

            return DecodeArtist(value);
        }

        public async Task<Artist> CreateOrUpdateArtist(ArtistMessage artistMessage, string id = default)
        {
            var artistFromUser = _mapper.Map<Artist>(artistMessage);

            var value = id == default
                ? await _faunaClient.Query(
                    Create(
                        Collection(Collection),
                        Obj("data", Encoder.Encode(artistFromUser))
                    )
                )
                : await _faunaClient.Query(
                    Replace(
                        Ref(Collection(Collection), id),
                        Obj("data", Encoder.Encode(artistFromUser))));

            return DecodeArtist(value);
        }

        public async Task DeleteArtistAsync(string id)
        {
            await _faunaClient.Query(Delete(Ref(Collection(Collection), id)));
        }

        private Artist DecodeArtist(Value value)
        {
            var artist = Decoder.Decode<Artist>(value.At("data"));
            var reference = value.At("ref").To<RefV>().Value;
            artist.Id = reference.Id;
            
            return artist;
        }
    }
}