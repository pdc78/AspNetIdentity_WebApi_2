using AspNetIdentity_WebApi.Data.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentity_WebApi.Data.Repository.IRepository
{
    public interface IRecipientRepository
    {
        IQueryable<Recipient> GetAll();

        Recipient Get(Guid idRecipient);
        Task<Recipient> GetAsync(Guid idRecipient);

        void Post(ref Recipient item);
        Task<string> PostAsync(Recipient item);

        bool Put(Recipient item);
        Task<bool> PutAsync(Recipient item);

        Task<bool> DeleteAsync(Recipient item);
    }
}
