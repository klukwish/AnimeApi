using Microsoft.AspNetCore.Mvc;
using AnimeApi.Models;
using AnimeApi.Clients;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeByNameController : ControllerBase
    {
        [HttpGet(Name = "AnimeFull")]
        public async Task<ActionResult<Anime>> AnimebyName(string name)
        {
            Client client = new Client();
            var result = client.GetAnimeByNameAsync(name).Result;

            if(result == null)
                return BadRequest(error: "Not found!");

            return Ok(result);
        }

    }


}
