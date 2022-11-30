using Microsoft.EntityFrameworkCore;
using CrudPersonasWebApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPersonasWebApi.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> GetPersonsById(int PersonId);
        Task<int> AddPerson(Person person);
        Task<int> DeletePerson(Person person);
        Task<int> UpdatePerson(Person person);
    }
    public class PersonService : IPersonService
    {
        private readonly CrudPersonasWebApiContext pruebaContext;

        public PersonService(CrudPersonasWebApiContext pruebaContext)
        {
            this.pruebaContext = pruebaContext;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await this.pruebaContext.Persons.Include(p => p.ContactMeansPeople).ThenInclude(c => c.ContactMeans).ToListAsync();
        }

        public async Task<Person> GetPersonsById(int PersonId)
        {
            return await this.pruebaContext.Persons.Include(p => p.ContactMeansPeople).ThenInclude(c => c.ContactMeans).FirstOrDefaultAsync(p => p.PersonId == PersonId);
        }

        public async Task<int> AddPerson(Person person)
        {
            await this.pruebaContext.Persons.AddAsync(person);
            await this.pruebaContext.SaveChangesAsync();
            return person.PersonId;
        }

        public async Task<int> UpdatePerson(Person person)
        {
            this.pruebaContext.Attach(person).State = EntityState.Modified;
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<int> DeletePerson(Person person)
        {
            this.pruebaContext.Attach(person).State = EntityState.Deleted;
            return await this.pruebaContext.SaveChangesAsync();
        }

    }
}
