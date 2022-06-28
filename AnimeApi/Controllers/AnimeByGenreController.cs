using Microsoft.AspNetCore.Mvc;
using AnimeApi.Models;
using AnimeApi.Clients;


namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeByGenreController : ControllerBase
    {

        [HttpGet(Name = "AnimeByGenres")]
        public async Task<ActionResult<AnimeList>> AnimebyGenres(int genre)
        {
            Client client = new Client();
            var result = client.GetAnimeByGenre(genre).Result;
            return Ok(result);
        }

  
    }
}
