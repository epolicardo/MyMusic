using System.Collections.Generic;
using System.Threading.Tasks;
using MyMusic.Core.Models;

namespace MyMusic.Core.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetById(int id);
        //Task<IEnumerable<Person>> GetMusicByArtistId(int id);
        Task<Person> CreatePerson(Person newPerson);
        Task Update(Person personToBeUpdated, Person person);
        Task Delete(Person person);

    }
}