using Microsoft.AspNetCore.Mvc;
using AnimeApi.Models;
using AnimeApi.Clients;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeByRandomGenreController : Controller
    {
        [HttpGet(Name = "AnimeByGenresRandom")]
        public async Task<AnimeList> AnimebyGenresRandom()
        {
            Random random = new Random();
            int genre;
            do
            {
                genre = random.Next(1, 49);
            }
            while (genre == 16 || genre == 12 || genre == 33 || genre == 34 || genre == 44 || genre == 45);
            Client client = new Client();
            return client.GetAnimeByGenre(genre).Result;
        }
    }
}
