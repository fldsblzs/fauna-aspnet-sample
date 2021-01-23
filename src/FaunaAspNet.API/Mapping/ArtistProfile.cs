using AutoMapper;
using FaunaAspNet.API.Messages;
using FaunaAspNet.API.Models;

namespace FaunaAspNet.API.Mapping
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<ArtistMessage, Artist>();
            CreateMap<AlbumMessage, Album>();
            CreateMap<MemberMessage, Member>();
            CreateMap<TrackMessage, Track>();
        }
    }
}