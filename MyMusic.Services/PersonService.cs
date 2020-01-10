
using System.Collections.Generic;
using System.Threading.Tasks;
using MyMusic.Core;
using MyMusic.Core.Models;
using MyMusic.Core.Services;

namespace MyMusic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Person> CreatePerson(Person newPerson)
        {
            await _unitOfWork.People
                .AddAsync(newPerson);
            
            return newPerson;
        }

        public async Task Delete(Person person)
        {
            _unitOfWork.People.Remove(person);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _unitOfWork.People.GetAllAsync();
        }

        public async Task<Person> GetById(int id)
        {
            return await _unitOfWork.People.GetByIdAsync(id);
        }

        public async Task Update(Person personToBeUpdated, Person person)
        {
            personToBeUpdated.Name = person.Name;

            await _unitOfWork.CommitAsync();
        }
    }
}