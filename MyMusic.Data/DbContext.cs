using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Data.Configurations;


namespace MyMusic.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Person> People { get; set; }


        public DbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .ApplyConfiguration(new MusicConfiguration());
            builder
            .ApplyConfiguration(new PersonConfiguration());
            builder
            .ApplyConfiguration(new ArtistConfiguration());
        }


    }
}