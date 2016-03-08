using AspNetIdentity_WebApi.Data.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentity_WebApi.Data.Repository.IRepository
{
    public interface IRecipientGroupRepository
    {
        IQueryable<Recipient_Group> GetAll();

        Recipient_Group Get(Guid idGroup,Guid idRecipient);
        Task<Recipient_Group> GetAsync(Guid idRecipient, Guid idGroup);

        Recipient_Group Post(Recipient_Group item);
        Task<Recipient_Group> PostAsync(Recipient_Group item);

        bool Put(Recipient_Group item);
        Task<bool> PutAsync(Recipient_Group item);

        Task<bool> DeleteAsync(Recipient_Group item);

        Task<bool> SaveAllAsync();

        void Dispose();

    }
}
