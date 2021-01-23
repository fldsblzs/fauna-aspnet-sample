namespace FaunaAspNet.API.Configuration
{
    public class FaunaOptions
    {
        public const string Fauna = "Fauna";
        
        public string Endpoint { get; set; }
        public string Secret { get; set; }
    }
}