using MyMusic.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Repositories;
using MyMusic.Data.Repositories;

namespace MyMusic.Data
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        
        
        public MusicRepository(DbContext context):base(context){}

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync(){
            return await DbContext.Musics
            .Include(m => m.Artist)
            .ToListAsync();
        }    
           public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await DbContext.Musics
                .Include(m => m.Artist)
                .SingleOrDefaultAsync(m => m.Id == id);;
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await DbContext.Musics
                .Include(m => m.Artist)
                .Where(m => m.ArtistId == artistId)
                .ToListAsync();
        }
        
        private DbContext DbContext
        {
            get { return context as DbContext; }
        }
    }
}