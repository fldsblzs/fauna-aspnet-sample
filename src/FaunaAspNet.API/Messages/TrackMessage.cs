namespace FaunaAspNet.API.Messages
{
    public class TrackMessage
    {
        public byte Order { get; set; }
        public string Title { get; set; }
        public byte Length { get; set; }
    }
}