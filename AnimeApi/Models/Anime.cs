namespace AnimeApi.Models
{

    public class Anime
    {
        public List<Data> data { get; set; }

        public List<string> listofgenres { get; set; }

        public string status { get; set; }

        public int ova_count { get; set; }

        public string main_title { get; set; }

    }
    public class Data
    {
        public int id { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }

    }

    public class Attributes
    {
        public string startDate { get; set; }

        public string endDate { get; set; }

        public string synopsis { get; set; }

        public string averageRating { get; set; }

        public string ageRating { get; set; }

        public string status { get; set; }

        public PosterImage posterImage { get; set; }

        public string episodeCount { get; set; }

        public string nextRelease { get; set; }

        public string subtype { get; set; }

        public Titles titles { get; set; }
    }
    public class Titles
    {
        public string en_jp { get; set; }
    }
    public class PosterImage
    {
        public string medium { get; set; }
    }

    public class Relationships
    {
        public Genres genres { get; set; }
    }
    public class Links
    {
        public string related { get; set; }
    }
    public class Genres
    {
        public Links links { get; set; }

        public List<genre> data { get; set; }
    }
    public class genre
    {
        public attributes attributes { get; set; }
    }

    public class attributes
    {
        public string name { get; set; }
    }

}

