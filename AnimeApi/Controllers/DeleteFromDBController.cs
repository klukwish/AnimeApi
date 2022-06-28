using AnimeApi.Clients;
using AnimeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteFromDBController : ControllerBase
    {
        private readonly IDynamoDBClient _dynamoDBClient;

        public DeleteFromDBController(IDynamoDBClient dynamoDBClient)
        {
            _dynamoDBClient = dynamoDBClient;
        }

        [HttpDelete(Name = "Delete data in DB")]
        public async Task<IActionResult> DeleteDataInDB(string user_id, string main_title)
        {
            var data = new DataForDB
            {
                user_id = user_id,
                main_title = main_title
            };
            var result = await _dynamoDBClient.DeleteAnimeFromDB(user_id, main_title);

            if (result == false)
            {
                return BadRequest("Cannot delete value in database. Please see console log");
            }
            return Ok("Value has been successfully deleted");
        }
    }
}
