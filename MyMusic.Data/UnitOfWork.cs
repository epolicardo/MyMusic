using System;
using System.Threading.Tasks;
using MyMusic.Core;
using MyMusic.Core.Repositories;
using MyMusic.Data.Repositories;

namespace MyMusic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private MusicRepository _musicRepository;
        private ArtistRepository _artistRepository;
        private PersonRepository _personRepository;

        

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }


        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);
        public IPersonRepository People => _personRepository = _personRepository ?? new PersonRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}