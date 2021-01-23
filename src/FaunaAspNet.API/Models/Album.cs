using System.Collections.Generic;

namespace FaunaAspNet.API.Models
{
    public class Album
    {
        public string Title { get; set; }
        public short Year { get; set; }
        public ushort Length { get; set; }
        public List<Track> Tracks { get; set; }
    }
}