using FaunaAspNet.API.Messages;
using FaunaAspNet.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FaunaAspNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist([FromRoute] string id)
        {
            var artist = await _artistRepository.GetArtistAsync(id);

            return Ok(artist);
        }

        [HttpGet("name/{artistName}")]
        public async Task<IActionResult> GetArtistByName([FromRoute] string artistName)
        {
            var artist = await _artistRepository.GetArtistByNameAsync(artistName);

            return Ok(artist);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await _artistRepository.GetArtistsAsync();
            
            return Ok(artists);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddArtist(ArtistMessage artistMessage)
        {
            var artist = await _artistRepository.CreateOrUpdateArtist(artistMessage);

            return CreatedAtAction(nameof(GetArtist), new {id = artist.Id}, artist);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(
            [FromRoute] string id,
            [FromBody] ArtistMessage artistMessage)
        {
            var artist = await _artistRepository.CreateOrUpdateArtist(artistMessage, id);

            return AcceptedAtAction(nameof(GetArtist), new {id}, artist);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] string id)
        {
            await _artistRepository.DeleteArtistAsync(id);

            return NoContent();
        }
    }
}