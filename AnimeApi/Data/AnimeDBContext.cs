using AnimeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeApi.Data
{
    public class AnimeDBContext: DbContext
    {
        public AnimeDBContext(DbContextOptions<AnimeDBContext> options)
            : base(options)
        {
        }

        public DbSet<DataForDB> Animes { get; set; }
    }
}
