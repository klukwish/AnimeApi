namespace AnimeApi.Models
{
    public class Top
    {
        public List<anime> top { get; set; }
    }
    public class anime
    {
        public string title { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }
    }
}
