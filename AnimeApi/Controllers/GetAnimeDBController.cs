using Amazon.DynamoDBv2.DataModel;
using AnimeApi.Clients;
using AnimeApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAnimeDBController : ControllerBase
    {
        private readonly IDynamoDBClient _dynamoDBClient;

        public GetAnimeDBController(IDynamoDBClient dynamoDBClient)
        {
            _dynamoDBClient = dynamoDBClient;
        }

        [HttpGet(Name = "AnimeFromDB")]
        public async Task<DataForDB> GetAnimeFromDB([FromQuery] string main_title, [FromQuery] string user_id)
        {
            var result = await _dynamoDBClient.GetAnimeFromDB(user_id,main_title);

            if (result == null)
                return null;

            return result;
        }

    }
}
