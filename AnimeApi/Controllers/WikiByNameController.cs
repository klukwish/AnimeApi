using Microsoft.AspNetCore.Mvc;
using AnimeApi.Clients;
using AnimeApi.Models;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WikiByNameController : ControllerBase
    {
        [HttpGet(Name = "WikiByName")]
        public async Task<ActionResult<Wiki>> WikiResultByName(string name)
        {
            Client client = new Client();
            var result = client.GetWikiByName(name).Result;
            if (result == null)
            {
                return BadRequest(error: "Not found!");
            }

            return Ok(result);
        }
    }
}
