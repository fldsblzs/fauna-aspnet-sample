using FaunaDB.Types;
using System.Collections.Generic;

namespace FaunaAspNet.API.Models
{
    public class Artist
    {
        [FaunaIgnore]
        public string Id { get; set; }
        
        [FaunaField("name")]
        public string Name { get; set; }

        [FaunaField("founded")]
        public short Founded { get; set; }
        
        [FaunaField("albums")]
        public List<Album> Albums { get; set; }

        [FaunaField("numberOfAlbums")]
        public byte NumberOfAlbums { get; set; }
        
        [FaunaField("members")]
        public List<Member> Members { get; set; }

        [FaunaField("numberOfMembers")]
        public byte NumberOfMembers { get; set; }
        
        [FaunaConstructor]
        public Artist(
            string name,
            short founded,
            List<Album> albums,
            List<Member> members)
        {
            Name = name;
            Founded = founded;
            Albums = albums;
            NumberOfAlbums = (byte)albums.Count;
            Members = members;
            NumberOfMembers = (byte)members.Count;
        }
    }
}