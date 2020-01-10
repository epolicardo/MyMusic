using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;

namespace MyMusic.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context) { }

        //public async Task<IEnumerable<Person>> GetAll()
        //{
        //    return await DbContext.People
        //        .Include(a => a.Name)
        //        .ToListAsync();
        //}

        //public Task<Artist> GetWithMusicsByIdAsync(int id)
        //{
        //    return DbContext.Artists
        //        .Include(a => a.Musics)
        //        .SingleOrDefaultAsync(a => a.Id == id);
        //}

        private DbContext DbContext
        {
            get { return context as DbContext; }
        }
    }
}