using AnimeApi.Constant;
using AnimeApi.Models;
using Newtonsoft.Json;

namespace AnimeApi.Clients
{
    public class Client
    {
        private HttpClient _httpClient;
        private string _address;
        private string _apikey;

        public Client()
        {
            _address = Constants._address;
            _apikey = Constants._apikey;
            _httpClient = new HttpClient();
        }

        public async Task<Anime> GetAnimeByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"https://kitsu.io/api/edge/anime?filter[text]={name}");
            var body = await response.Content.ReadAsStringAsync();
            Anime anime = new Anime();
            anime = JsonConvert.DeserializeObject<Anime>(body);

            if(anime.data.Count() == 0)
            {
                return null;
            }

            response = await _httpClient.GetAsync(anime.data[0].relationships.genres.links.related);
            body = await response.Content.ReadAsStringAsync();
            Genres genres = new Genres();
            genres = JsonConvert.DeserializeObject<Genres>(body);

            anime.listofgenres = new List<string>();
            foreach (genre g in genres.data)
            {
                anime.listofgenres.Add(g.attributes.name);
            }

            for (int i = 0; i < anime.data.Count(); i++)
            {
                if (anime.data[i].attributes.subtype == "OVA" || anime.data[i].attributes.subtype == "special")
                {
                    anime.ova_count++;
                }
            }

            for (int i = 0; i < anime.data.Count(); i++)
            {
                if (anime.data[i].attributes.status != "finished" || anime.data[i].attributes.nextRelease != null)
                {
                    anime.status = "Not finished";
                    break;
                }
                else
                {
                    anime.status = "Finished";
                }
            }

            anime.main_title = anime.data[0].attributes.titles.en_jp;


            return anime;
        }

        public async Task<AnimeList> GetAnimeByGenre(int genre)
        {
            int page;
            int n = 0;
            switch (genre)
            {
                case 1:
                    n = 43;
                    break;
                case 2:
                    n = 34;
                    break;
                case 3:
                    n = 2;
                    break;
                case 4:
                    n = 65;
                    break;
                case 5:
                    n = 6;
                    break;
                case 6:
                    n = 6;
                    break;
                case 7:
                    n = 8;
                    break;
                case 8:
                    n = 27;
                    break;
                case 9:
                    n = 8;
                    break;
                case 10:
                    n = 40;
                    break;
                case 11:
                    n = 4;
                    break;
                case 13:
                    n = 14;
                    break;
                case 14:
                    n = 5;
                    break;
                case 15:
                    n = 41;
                    break;
                case 17:
                    n = 6;
                    break;
                case 18:
                    n = 12;
                    break;
                case 19:
                    n = 27;
                    break;
                case 20:
                    n = 7;
                    break;
                case 21:
                    n = 3;
                    break;
                case 22:
                    n = 20;
                    break;
                case 23:
                    n = 18;
                    break;
                case 24:
                    n = 28;
                    break;
                case 25:
                    n = 7;
                    break;
                case 26:
                    n = 2;
                    break;
                case 27:
                    n = 21;
                    break;
                case 28:
                    n = 2;
                    break;
                case 29:
                    n = 6;
                    break;
                case 30:
                    n = 7;
                    break;
                case 31:
                    n = 6;
                    break;
                case 32:
                    n = 2;
                    break;
                case 35:
                    n = 4;
                    break;
                case 36:
                    n = 20;
                    break;
                case 37:
                    n = 16;
                    break;
                case 38:
                    n = 7;
                    break;
                case 39:
                    n = 3;
                    break;
                case 40:
                    n = 4;
                    break;
                case 41:
                    n = 2;
                    break;
                case 42:
                    n = 9;
                    break;
                case 43:
                    n = 1;
                    break;
                case 46:
                    n = 1;
                    break;
                case 47:
                    n = 2;
                    break;
                case 48:
                    n = 2;
                    break;
            }

            Random rnd = new Random();
            page = rnd.Next(1, n + 1);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/genre/anime/{genre}/{page}"),
                Headers =
    {
                { "X-RapidAPI-Host", "jikan1.p.rapidapi.com" },
                { "X-RapidAPI-Key", _apikey },
    },
            };
            AnimeList anime_list = new AnimeList();
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                anime_list = JsonConvert.DeserializeObject<AnimeList>(body);
            }


            return anime_list;
        }

        public async Task<Top> GetTopAnime(string subtype)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address+ $"/top/anime/1/{subtype}"),
                Headers =
                 {
                { "X-RapidAPI-Key", _apikey},
                { "X-RapidAPI-Host", "jikan1.p.rapidapi.com" },
                },
            };
            Top top_anime = new Top();
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                top_anime = JsonConvert.DeserializeObject<Top>(body);
            }

            foreach (anime a in top_anime.top)
            {
                Console.WriteLine(a.title);
            }
            return top_anime;
        }
        public async Task<Wiki> GetWikiByName(string search_word)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://wiki-briefs.p.rapidapi.com/search?q="+search_word+ "&topk=3"),
                Headers =
                {
                { "X-RapidAPI-Key", _apikey },
                { "X-RapidAPI-Host", "wiki-briefs.p.rapidapi.com" },
                },
            };

            Wiki wikiresult = new Wiki();
            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                var body = await response.Content.ReadAsStringAsync();
                wikiresult = JsonConvert.DeserializeObject<Wiki>(body);
            }
            return wikiresult;
        }
    }
}
