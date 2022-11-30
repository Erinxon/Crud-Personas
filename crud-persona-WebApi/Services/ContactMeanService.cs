using Microsoft.EntityFrameworkCore;
using CrudPersonasWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudPersonasWebApi.Services
{
    public interface IContactMeanService
    {
        Task<IEnumerable<ContactMean>> GetContactMean();
        Task<ContactMean> GetContactMeanById(int ContactMeansId);
        Task<int> AddContactMean(ContactMean contactMean);
        Task<int> updateContactMean(ContactMean contactMean);
        Task<int> DeleteContactMean(ContactMean contactMean);
    }
    public class ContactMeanService : IContactMeanService
    {
        private readonly CrudPersonasWebApiContext pruebaContext;

        public ContactMeanService(CrudPersonasWebApiContext pruebaContext)
        {
            this.pruebaContext = pruebaContext;
        }

        public async Task<IEnumerable<ContactMean>> GetContactMean()
        {
            return await this.pruebaContext.ContactMeans.ToListAsync();
        }

        public async Task<ContactMean> GetContactMeanById(int ContactMeansId)
        {
            return await this.pruebaContext.ContactMeans.FirstOrDefaultAsync(c => c.ContactMeansId == ContactMeansId);
        }

        public async Task<int> AddContactMean(ContactMean contactMean)
        {
            await this.pruebaContext.ContactMeans.AddAsync(contactMean);
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<int> updateContactMean(ContactMean contactMean)
        {
            this.pruebaContext.Attach(contactMean).State = EntityState.Modified;
            return await this.pruebaContext.SaveChangesAsync();
        }

        public async Task<int> DeleteContactMean(ContactMean contactMean)
        {
            this.pruebaContext.Attach(contactMean).State = EntityState.Deleted;
            return await this.pruebaContext.SaveChangesAsync();
        }
    }
}
