using AspNetIdentity_WebApi.Data.Repository.IRepository;
using System;
using System.Linq;
using AspNetIdentity_WebApi.Data.Entity;
using System.Threading.Tasks;
using AspNetIdentity_WebApi.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AspNetIdentity_WebApi.Data.Repository
{
    public class RecipientGroupRepository : IRecipientGroupRepository
    {
        private readonly ApplicationDbContext _context; 
        private bool _disposed;

        #region constructor

        public RecipientGroupRepository()
        {
            _context = new ApplicationDbContext();
        }

        public RecipientGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public IQueryable<Recipient_Group> GetAll()
        {
            return _context.RecipientsGroup;
        }

        // Get Recipient_Group by Id
        public Recipient_Group Get(Guid idRecipient, Guid idGroup)
        {
           return _context.RecipientsGroup.Find(idRecipient,idGroup);
        }

        public async Task<Recipient_Group> GetAsync(Guid idRecipient, Guid idGroup)
        {
            return await _context.RecipientsGroup.FindAsync(idRecipient, idGroup);
        }

        // Create Recipient_Group element
        public Recipient_Group Post(Recipient_Group item)
        {
            // do you need to call afther to call method SaveAllAsync
            try
            {
                _context.RecipientsGroup.Add(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return item;
        }

        public async Task<Recipient_Group> PostAsync(Recipient_Group item)
        {
            try
            {
                _context.RecipientsGroup.Add(item);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return item;
         
        }

        // Update Recipient_Group element
        public bool Put(Recipient_Group item)
        {
            // do you need to call afther to call method SaveAllAsync
            try
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> PutAsync(Recipient_Group item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        // Delete Recipient_Group element
        public async Task<bool> DeleteAsync(Recipient_Group item)
        {
            _context.RecipientsGroup.Remove(item);

            try 
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        // Method to save modified of EF and you have to call 
        // after you call Put() and Post() methods
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources
            Dispose(true);
            // Suppress finalization
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free any other managed objects here
                    _context.Dispose();
                }
            }

            // Free any unmanaged objects here
            _disposed = true;
        }

    }
}