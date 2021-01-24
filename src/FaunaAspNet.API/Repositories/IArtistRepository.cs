using FaunaAspNet.API.Messages;
using FaunaAspNet.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaunaAspNet.API.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistAsync(string id);
        
        Task<Artist> GetArtistByNameAsync(string name);

        Task<IEnumerable<Artist>> GetArtistsAsync();
        
        Task<Artist> CreateOrUpdateArtist(ArtistMessage artistMessage, string id = default);
        
        Task DeleteArtistAsync(string id);
    }
}