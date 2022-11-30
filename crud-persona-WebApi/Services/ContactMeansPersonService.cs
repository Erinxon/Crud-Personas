using Microsoft.EntityFrameworkCore;
using CrudPersonasWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPersonasWebApi.Services
{
    public interface IContactMeansPersonService
    {
        Task<IEnumerable<ContactMeansPerson>> GetContactMeansPeople();
        Task<ContactMeansPerson> GetContactMeansPeopleById(int Id);
        Task<IEnumerable<ContactMeansPerson>> GetContactMeansPeopleByPersonId(int PersonId);
        Task<int> AddContactMeansPerson(List<ContactMeansPerson> contactMeansPerson);
        Task<int> UpdateContacMeanPerson(ContactMeansPerson contactMeansPerson);
        Task<int> UpdateContacMeanPerson(List<ContactMeansPerson> contactMeansPerson);
        Task<int> Delete(int Id);
        Task<int> DeleteAllByPersonId(int PersonId);
    }
    public class ContactMeansPersonService : IContactMeansPersonService
    {
        private readonly CrudPersonasWebApiContext pruebaContext;

        public ContactMeansPersonService(CrudPersonasWebApiContext pruebaContext)
        {
            this.pruebaContext = pruebaContext;
        }

        public async Task<int> AddContactMeansPerson(List<ContactMeansPerson> contactMeansPerson)
        {
            await this.pruebaContext.ContactMeansPeople.AddRangeAsync(contactMeansPerson);
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactMeansPerson>> GetContactMeansPeople()
        {
            return await this.pruebaContext.ContactMeansPeople.Include(c => c.ContactMeans).ToListAsync();
        }

        public async Task<IEnumerable<ContactMeansPerson>> GetContactMeansPeopleByPersonId(int PersonId)
        {
            return await this.pruebaContext.ContactMeansPeople.Where(c => c.PersonId == PersonId).ToListAsync();
        }
        public async Task<int> DeleteAllByPersonId(int PersonId)
        {
            var contactMeansPeople = await this.GetContactMeansPeopleByPersonId(PersonId);
            this.pruebaContext.ContactMeansPeople.RemoveRange(contactMeansPeople);
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var contactMeans = await this.GetContactMeansPeopleById(Id);
            this.pruebaContext.Attach(contactMeans).State = EntityState.Deleted;
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<ContactMeansPerson> GetContactMeansPeopleById(int Id)
        {
            return await this.pruebaContext.ContactMeansPeople.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<int> UpdateContacMeanPerson(ContactMeansPerson contactMeansPerson)
        {
            this.pruebaContext.Attach(contactMeansPerson).State = EntityState.Modified;
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<int> UpdateContacMeanPerson(List<ContactMeansPerson> contactMeansPerson)
        {
            this.pruebaContext.UpdateRange(contactMeansPerson);
            return await this.pruebaContext.SaveChangesAsync();
        }
    }
}
