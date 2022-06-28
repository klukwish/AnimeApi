using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using AnimeApi.Extensions;
using AnimeApi.Models;
using System.Text.RegularExpressions;

namespace AnimeApi.Clients
{
    public class DynamoDBClient:IDynamoDBClient
    {
        public string _tableName { get; set; }
        private readonly IAmazonDynamoDB _dynamoDB;

        public DynamoDBClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
            _tableName = Constant.Constants.TableName;
        }
        public async Task<DataForDB> GetAnimeFromDB(string user_id, string main_title)
        {
            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"user_id", new AttributeValue{S = user_id} },
                    {"main_title", new AttributeValue{S = main_title } }
                }
            };
            var response = await _dynamoDB.GetItemAsync(item);

            if (response.Item == null ||  !response.IsItemSet)
                return null;

            var result = response.Item.ToClass<DataForDB>();

            return result;
        }

        public async Task<bool> PostDataToDB(DataForDB dataForDB)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"user_id", new AttributeValue{S = dataForDB.user_id} },
                    {"main_title", new AttributeValue {S= dataForDB.main_title } }
                }
            };

            try
            {
                var response = await _dynamoDB.PutItemAsync(request);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
                return false;
            }
        }

        public async Task<List<DataForDB>> GetAll()
        {
            var result = new List<DataForDB>();
            var request = new ScanRequest
            {
                TableName = _tableName
            };
            var response =await _dynamoDB.ScanAsync(request);
            if (response.Items == null || response.Items.Count == 0)
            {
                return null;
            }

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<DataForDB>());
            }
            return result;
        }

        public async Task<bool> DeleteAnimeFromDB(string user_id, string main_title)
        {
            var request = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>() 
                { 
                    { "user_id", new AttributeValue { S = user_id } },
                    {"main_title", new AttributeValue {S = main_title} }
                }
            };

            try
            {
                var response =await _dynamoDB.DeleteItemAsync(request);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error \n" + e);
                return false;
            }
        }

        public async Task<Anime> CheckDateOfNextRelease(string user_id)
        {
            List<DataForDB> allFavorite = await GetAll();

            List<DataForDB> users_favorite = new List<DataForDB>();
            foreach (DataForDB dataForDB in allFavorite)
            {
                if (dataForDB.user_id == user_id)
                {
                    users_favorite.Add(dataForDB);
                }
            }

            List<Anime> list_of_user_anime = new List<Anime>();
            foreach(DataForDB dataforbd in users_favorite)
            {
                Client client = new Client();
                Anime anime = new Anime();
                anime = await client.GetAnimeByNameAsync(dataforbd.main_title);
                list_of_user_anime.Add(anime);
            }

            foreach(Anime a in list_of_user_anime)
            {
                for(int i = 0; i<a.data.Count(); i++)
                {
                    if (a.data[i].attributes.nextRelease != null)
                    {
                        DateTime now = DateTime.Now;
                        string text = now.Date.ToString();
                        Regex rg = new Regex(@"^[0-9]+\.[0-9]+\.[0-9]+");
                        MatchCollection match = rg.Matches(text);
                        string date_now = match[0].Value;

                        string next_release = a.data[i].attributes.nextRelease;
                        Regex regex = new Regex(@"^([0-9]+\W*)+");
                        MatchCollection matches = regex.Matches(next_release);
                        List<string> list = new List<string>();
                        foreach (Match m in matches)
                        {
                            list.Add(m.Value);
                        }
                        regex = new Regex(@"[0-9]+");
                        matches = regex.Matches(list[0]);

                        List<string> list2 = new List<string>();
                        foreach (Match m2 in matches)
                        {
                            list2.Add(m2.Value);
                        }
                        string date_of_release = list2[2] + "." + list2[1] + "." + list2[0];

                        if(date_of_release == date_now)
                        {
                            return a;
                        }
                    }
                }
            }
            return null;
        }
    }
}
