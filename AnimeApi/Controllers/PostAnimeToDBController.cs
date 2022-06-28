using AnimeApi.Clients;
using AnimeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimeApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PostAnimeToDBController : ControllerBase
    {
        private readonly IDynamoDBClient _dynamoDBClient;

        public PostAnimeToDBController(IDynamoDBClient dynamoDBClient)
        {
            _dynamoDBClient = dynamoDBClient;
        }

        [HttpPost(Name = "PostData")]
        public async Task<IActionResult> AddToFavorItems([FromBody] DataForDB dataForDB)
        {
            var data = new DataForDB
            {
                user_id = dataForDB.user_id,
                main_title = dataForDB.main_title
            };
            var result = await _dynamoDBClient.PostDataToDB(data);

            if (result == false)
            {
                return BadRequest("Cannot insert value in database. Please see console log");
            }
            return Ok("Value has been successfully added to DB");
        }
    }
}
