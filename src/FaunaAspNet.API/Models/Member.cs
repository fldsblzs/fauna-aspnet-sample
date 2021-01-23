using System.Collections.Generic;

namespace FaunaAspNet.API.Models
{
    public class Member
    {
        public string Name { get; set; }
        public short Joined { get; set; }
        public List<string> Instruments { get; set; }
    }
}