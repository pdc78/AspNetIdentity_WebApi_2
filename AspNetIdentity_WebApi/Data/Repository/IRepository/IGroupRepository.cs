using AspNetIdentity_WebApi.Data.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentity_WebApi.Data.Repository.IRepository
{
    public interface IGroupRepository
    {
        IQueryable<Group> GetAll();

        Group Get(Guid idGroup);
        Task<Group> GetAsync(Guid idGroup);

        void Post(ref Group item);
        //Task<string> PostAsync(Group item);
        Task<Group> PostAsync(Group item);

        bool Put(Group item);
        Task<bool> PutAsync(Group item);

        Task<bool> DeleteAsync(Group item);
    }
}

