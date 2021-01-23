using System.Collections.Generic;

namespace FaunaAspNet.API.Messages
{
    public class MemberMessage
    {
        public string Name { get; set; }
        public short Joined { get; set; }
        public List<string> Instruments { get; set; }
    }
}