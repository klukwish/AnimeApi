using Microsoft.AspNetCore.Mvc;
using AnimeApi.Models;
using AnimeApi.Clients;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopAnimeController : Controller
    {
        [HttpGet(Name = "TopAnime")]
        public async Task<Top> AnimeTop(string subtype)
        {
            Client client = new Client();
            return client.GetTopAnime(subtype).Result;
        }
    }
}
