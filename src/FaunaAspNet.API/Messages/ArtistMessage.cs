using System.Collections.Generic;

namespace FaunaAspNet.API.Messages
{
    public class ArtistMessage
    {
        public string Name { get; set; }
        public short Founded { get; set; }
        public List<AlbumMessage> Albums { get; set; }
        public List<MemberMessage> Members { get; set; }
    }
}