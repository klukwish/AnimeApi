using AnimeApi.Models;

namespace AnimeApi.Clients
{
    public interface IDynamoDBClient
    {
        public Task<DataForDB> GetAnimeFromDB(string user_id, string main_title);

        public Task<bool> PostDataToDB(DataForDB dataForDB);

        public Task<List<DataForDB>> GetAll();

        public Task<bool> DeleteAnimeFromDB(string user_id, string main_title);

        public Task<Anime> CheckDateOfNextRelease(string user_id);
    }
}
