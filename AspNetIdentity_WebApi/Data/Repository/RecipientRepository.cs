using System;
using System.Linq;
using AspNetIdentity_WebApi.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using AspNetIdentity_WebApi.Data.Entity;
using AspNetIdentity_WebApi.Data.Repository.IRepository;

namespace AspNetIdentity_WebApi.Data.Repository
{
    public class RecipientRepository : IRecipientRepository
    {

        private readonly ApplicationDbContext _dbContext; // = new ApplicationDbContext();

        #region constructor

        public RecipientRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public RecipientRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        #endregion

        // Get All Recipients
        IQueryable<Recipient> IRecipientRepository.GetAll()
        {
            return _dbContext.Recipients;
        }

        // Get Recipient By Id
        Recipient IRecipientRepository.Get(Guid idRecipient)
        {
            return  GetRecipientById(idRecipient);
        }

        async Task<Recipient> IRecipientRepository.GetAsync(Guid idRecipient)
        {
            return await _dbContext.Recipients.FindAsync(idRecipient);

        }

        //Create New Recipient
        void IRecipientRepository.Post(ref Recipient item)
        {

            _dbContext.Recipients.Add(item);
            try
            {
                _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
         }

        async Task<string> IRecipientRepository.PostAsync(Recipient item)
        {
            _dbContext.Recipients.Add(item);
            try
            {
               await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return "Recipient added successfully!";

        }

        //Update Recipient
        bool IRecipientRepository.Put(Recipient item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;

            try
            {
               _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipientExists(item.Id_Recipient))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        async Task<bool> IRecipientRepository.PutAsync(Recipient obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            try
            {
               await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RecipientExists(obj.Id_Recipient))
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                else
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        //Delete Recipient
        async Task<bool> IRecipientRepository.DeleteAsync(Recipient recipient)
        {
            _dbContext.Recipients.Remove(recipient);
            try {
                if (await _dbContext.SaveChangesAsync() == 1) {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        #region private method
        private Recipient GetRecipientById(Guid id)
        {
            return  _dbContext.Recipients.Find(id);
        }

        private bool RecipientExists(Guid id)
        {
            return _dbContext.Recipients.Count(e => e.Id_Recipient == id) > 0;
        }
        #endregion
    }
}