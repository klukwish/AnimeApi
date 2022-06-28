using AnimeApi.Clients;
using AnimeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemainderController : ControllerBase
    {
        private readonly IDynamoDBClient _dynamoDBClient;

        public RemainderController(IDynamoDBClient dynamoDBClient)
        {
            _dynamoDBClient = dynamoDBClient;
        }

        [HttpGet(Name = "Remainder")]
        public async Task<ActionResult<Anime>> GetRemainder(string user_id)
        {
            var result = await _dynamoDBClient.CheckDateOfNextRelease(user_id);

            if (result == null)
            {
                return BadRequest(error: "Not found!");
            }

            return Ok(result);
        }
    }
}
