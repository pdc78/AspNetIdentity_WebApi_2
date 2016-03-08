using AspNetIdentity_WebApi.Data.Repository.IRepository;
using System;
using System.Linq;
using AspNetIdentity_WebApi.Data.Entity;
using System.Data.Entity;
using AspNetIdentity_WebApi.Infrastructure;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace AspNetIdentity_WebApi.Data.Repository
{
    public class GroupRepository : IGroupRepository
    {

        private readonly ApplicationDbContext _context; // = new ApplicationDbContext();

        #region constructor

        public GroupRepository()
        {
            _context = new ApplicationDbContext();
        }

        public GroupRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        #endregion


        #region public method

        // Get All Groups
        IQueryable<Group> IGroupRepository.GetAll()
        {
            return _context.Groups;
        }

        // Get Recipient By Id
        Group IGroupRepository.Get(Guid idGroup)
        {
            return _context.Groups.Find(idGroup);
        }

        async Task<Group> IGroupRepository.GetAsync(Guid idGroup)
        {
            return await _context.Groups.FindAsync(idGroup);
        }

        // Create New Group
        void IGroupRepository.Post(ref Group item)
        {

            _context.Groups.Add(item);
            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        async Task<Group> IGroupRepository.PostAsync(Group item)
        {
            _context.Groups.Add(item);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new  Exception(ex.Message);
            }
            //return "Group added successfully!";
            return item;
        }

        // Update Group
        bool IGroupRepository.Put(Group item)
        {

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return true;
        }

        async Task<bool> IGroupRepository.PutAsync(Group obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        // Delete Group
        async Task<bool> IGroupRepository.DeleteAsync(Group item)
        {
            _context.Groups.Remove(item);
            try
            {
                if (await _context.SaveChangesAsync() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion
    }
}