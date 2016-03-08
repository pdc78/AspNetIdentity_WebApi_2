using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetIdentity_WebApi.Models;
using Microsoft.AspNet.Identity;
using AspNetIdentity_WebApi.Data.Entity;
using AspNetIdentity_WebApi.Data.Repository.IRepository;
using AspNetIdentity_WebApi.Data.Repository;

namespace AspNetIdentity_WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/recipients")]
    public class RecipientsController : BaseApiController
    {

        private IRecipientRepository _repositoryRecipient;

        #region constractor
        public RecipientsController()
        {
            _repositoryRecipient = new RecipientRepository();
        }

        public RecipientsController(IRecipientRepository repositoryRecipient)
        {
            _repositoryRecipient = repositoryRecipient;
        }
        #endregion

        #region public method CRUD


        // GET: api/Recipients
        [Route("recipients")]
        public IQueryable<Recipient> GetAllRecipients()
        {
            return _repositoryRecipient.GetAll();
        }



        [Authorize]
        // GET: api/Recipients/5
        [ResponseType(typeof(Recipient))]
        [Route("recipient/{id:guid}", Name = "GetRecipientById")]
        public IHttpActionResult GetRecipientById(Guid id)
        {
            var recipient =  _repositoryRecipient.Get(id);

            if (recipient == null)
            {
                return NotFound();
            }

            return Ok(recipient);
        }


        [Authorize]
        // GET: api/Recipients/5
        [ResponseType(typeof(Recipient))]
        [Route("recipient/{id:guid}", Name = "GetRecipient")]
        public async Task<IHttpActionResult> GetRecipient(Guid id)
        {
            var recipient = await _repositoryRecipient.GetAsync(id);

            if (recipient == null)
            {
                return NotFound();
            }

            return Ok(TheModelFactory.Create(recipient));
        }

        [Authorize]
        // GET: api/Recipients/5
        [ResponseType(typeof(Recipient))]
        [Route("recipient/{mobile}")]
        public IHttpActionResult GetRecipientByMobile(string mobile)
        {
            var recipient = _repositoryRecipient.GetAll().FirstOrDefault(c => c.MobileNumber == mobile);
            if (recipient == null)
            {
                return NotFound();
            }

            return Ok(recipient);
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/Recipients/5
        [ResponseType(typeof(void))]
        [Route("recipient/{id:guid}")]
        public async Task<IHttpActionResult> PutRecipient(Guid id, RecipientCreateModel modelRecipient)
        {
            if (ModelState.IsValid)
            {
                var recipient = await _repositoryRecipient.GetAsync(id);

                if (recipient == null)
                {
                    return BadRequest();
                }

                UpdateEntity(ref modelRecipient, ref recipient);

                recipient.Date_Modify = DateTime.Now;
                recipient.User_Id_Modify = new Guid(User.Identity.GetUserId());

                try
                {
                    if (await _repositoryRecipient.PutAsync(recipient))
                    {
                        return Ok();
                    }
                }
                catch (Exception)
                {
                    //throw ex;
                    return StatusCode(status: HttpStatusCode.InternalServerError);
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        // POST: api/Recipients
        [Route("create")]
        [ResponseType(typeof(Recipient))]
        public IHttpActionResult PostRecipient(RecipientCreateModel recipientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipient = new Recipient();

            UpdateEntity(ref recipientModel, ref recipient);
            recipient.Date_Created = DateTime.Now;
            recipient.User_Id_Created = new Guid(User.Identity.GetUserId());

            try
            {
                _repositoryRecipient.Post(ref recipient);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }

            // Uri locationHeader = new Uri(Url.Link("GetRecipient", new { id = recipient.Id_Recipient }));

            //return Created(locationHeader, recipient);
            return Ok(TheModelFactory.Create(recipient));
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/Recipients/5
        [ResponseType(typeof(Recipient))]
        [Route("recipient/{id:guid}")]
        public async Task<IHttpActionResult> DeleteRecipient(Guid id)
        {
            var recipient =  _repositoryRecipient.Get(id);

            if (recipient == null)
            {
                return NotFound();
            }

            if (await _repositoryRecipient.DeleteAsync(recipient))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
           
        }

        #endregion

        #region protected method
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void UpdateEntity(ref RecipientCreateModel recipientModel, ref Recipient entityRecipient )
        {

            if (entityRecipient.Email != recipientModel.Email)
                entityRecipient.Email = recipientModel.Email;
            if (entityRecipient.FirstName != recipientModel.FirstName)
                entityRecipient.FirstName = recipientModel.FirstName;
            if (entityRecipient.SecondName != recipientModel.SecondName)
                entityRecipient.SecondName = recipientModel.SecondName;
            if (entityRecipient.MobileNumber != recipientModel.MobileNumber)
                entityRecipient.MobileNumber = recipientModel.MobileNumber;
            if (entityRecipient.Address != recipientModel.Address)
                entityRecipient.Address = recipientModel.Address;
            if (entityRecipient.Birthday != recipientModel.Birthday)
                entityRecipient.Birthday = recipientModel.Birthday;
            if (entityRecipient.City != recipientModel.City)
                entityRecipient.City = recipientModel.City;
            if (entityRecipient.Country != recipientModel.Country)
                entityRecipient.Country = recipientModel.Country;
            if (entityRecipient.HomePhoneNumber != recipientModel.HomePhoneNumber)
                entityRecipient.HomePhoneNumber = recipientModel.HomePhoneNumber;
            if (entityRecipient.OfficePhoneNumber != recipientModel.OfficePhoneNumber)
                entityRecipient.OfficePhoneNumber = recipientModel.OfficePhoneNumber;
            if (entityRecipient.PostalCode != recipientModel.PostalCode)
                entityRecipient.PostalCode = recipientModel.PostalCode;
            if (entityRecipient.Province != recipientModel.Province)
                entityRecipient.Province = recipientModel.Province;
            if (entityRecipient.Active_Flg != recipientModel.ActiveFlg)
                entityRecipient.Active_Flg = recipientModel.ActiveFlg;

            //EntityRecipient.Date_Created = DateTime.Now;
            //EntityRecipient.User_Id_Created = new Guid(User.Identity.GetUserId());
            //EntityRecipient.Active_Flg = true;
        }
        #endregion
    }
}