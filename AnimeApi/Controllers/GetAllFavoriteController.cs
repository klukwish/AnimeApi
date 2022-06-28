using AnimeApi.Clients;
using AnimeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllFavoriteController : Controller
    {
        private readonly IDynamoDBClient _dynamoDBClient;

        public GetAllFavoriteController(IDynamoDBClient dynamoDBClient)
        {
            _dynamoDBClient = dynamoDBClient;
        }

        [HttpGet(Name = "All favorites")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dynamoDBClient.GetAll();

            if (response == null)
                return NotFound("There are no records in db");

            var result = response
                .Select(x => new DataForDB()
                {
                    user_id = x.user_id,
                    main_title = x.main_title
                })
                .ToList();

            return Ok(result);

        }
    }
}
