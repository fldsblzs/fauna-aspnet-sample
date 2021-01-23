using System.Collections.Generic;

namespace FaunaAspNet.API.Messages
{
    public class AlbumMessage
    {
        public string Title { get; set; }
        public short Year { get; set; }
        public ushort Length { get; set; }
        public List<TrackMessage> Tracks { get; set; }
    }
}