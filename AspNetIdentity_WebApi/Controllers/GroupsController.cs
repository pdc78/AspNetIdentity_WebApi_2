using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetIdentity_WebApi.Models;
using Microsoft.AspNet.Identity;
using AspNetIdentity_WebApi.Data.Entity;
using AspNetIdentity_WebApi.Data.Repository;
using AspNetIdentity_WebApi.Data.Repository.IRepository;


namespace AspNetIdentity_WebApi.Controllers
{
    [Authorize(Roles = "Admin,Advanced")]
    [RoutePrefix("api/groups")]
    public class GroupsController : BaseApiController //ApiController
    {
        private IGroupRepository _repositoryGroup;
        private IRecipientGroupRepository _repositoryRecipientGroup;

        #region constractor
        public GroupsController()
        {
            _repositoryGroup = new GroupRepository();
            _repositoryRecipientGroup = new RecipientGroupRepository();
        }

        public GroupsController(IGroupRepository repositoryGroup, IRecipientGroupRepository repositoryRecipientGroup) 
        {
            _repositoryGroup = repositoryGroup;
            _repositoryRecipientGroup = repositoryRecipientGroup;
        }
        #endregion

        // GET: api/Groups
        [Route("groups")]
        public IQueryable<Group> GetList_Groups()
        {
            return _repositoryGroup.GetAll();
        }

        // GET: api/Groups/5
        [Route("group/{id:guid}", Name = "GetGroup")]
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> GetGroup(Guid id)
        {
            //Group group = await db.List_Groups.FindAsync(id);

            Group group = await _repositoryGroup.GetAsync(id);
                //await GetGroupById(id);
            //group.List_of_Recipient_Group = db.List_Recipients_Group.Where(g => g.Id_Group == id);
            if (group == null)
            {
                return NotFound();
            }

            //return Ok(group);
            return Ok(TheModelFactory.Create(group));
        }

        //GET: api/Groups/5/recipients
        [Route("group/{id:guid}/recipients", Name = "GetGroupWithDetails")]
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> GetGroupWithDetails(Guid id)
        {
            Group group = await _repositoryGroup.GetAsync(id);
            
            if (group == null)
            {
                return NotFound();
            }

            IQueryable<Recipient> recipients = _repositoryRecipientGroup.GetAll().Where(g => g.Id_Group == id).Select(r => r.Recipient);
        
            return Ok(TheModelFactory.Create(group,recipients));
        }


        [Authorize]
        // GET: api/Recipients/5
        [ResponseType(typeof(Group))]
        [Route("recipient/{Name_Group}")]
        public IHttpActionResult GetGroupByName(string nameGroup)
        {
            if (nameGroup == null || string.IsNullOrEmpty(nameGroup))
                return NotFound();

            Group @group = _repositoryGroup.GetAll().Where(c => c.Name_Group == nameGroup).FirstOrDefault();

            if (@group == null)
                return NotFound();

            return Ok(@group);
        }


        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        [Route("group/{id:guid}")]
        public async Task<IHttpActionResult> PutGroup(Guid id, GroupUpdateModel groupModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groupModel.IdGroup)
            {
                return BadRequest();
            }

            Group @group = await _repositoryGroup.GetAsync(id);
            @group.Date_Modify = DateTime.Now;
            @group.User_Id_Modify = new Guid(User.Identity.GetUserId());
            @group.Description_Group = groupModel.DescriptionGroup;
            @group.Active_Flg = groupModel.ActiveFlg;

            // _group = await _repositoryGroup.PostAsync(_group);

            try
            {
                if(await _repositoryGroup.PutAsync(@group)){
                    return Ok(TheModelFactory.Create(@group));
                }
                //_resault = await _repositoryGroup.PutAsync(_group);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.BadRequest);
        }

        // POST: api/Groupsawait db.List_Recipients_Group.FindAsync(id, itemRecipientId)
        [ResponseType(typeof(Group))]
        [Route("create")]
        public async Task<IHttpActionResult> PostGroup(GroupCreateModel groupModel)
        {

            if (_repositoryGroup.GetAll().Where(c => c.Name_Group == groupModel.NameGroup).FirstOrDefault()!= null)
            {
                ModelState.AddModelError("Name_Group", "There is already a group with this name");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Group group = new Group();
            group.Active_Flg = groupModel.ActiveFlg;
            group.Description_Group = groupModel.DescriptionGroup;
            group.Name_Group = groupModel.NameGroup;
            group.Date_Created = DateTime.Now;
            group.User_Id_Created = new Guid(User.Identity.GetUserId());

            try {
                group = await _repositoryGroup.PostAsync(group);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return Ok(TheModelFactory.Create(group));

            //Uri locationHeader = new Uri(Url.Link("GetGroup", new { id = group.Id_Group }));

            //return Created(locationHeader, group);

            //return CreatedAtRoute("GetGroup", new { id = group.Id_Group }, group);
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        [Route("group/{id:guid}")]
        public async Task<IHttpActionResult> DeleteGroup(Guid id)
        {
            Group group = await _repositoryGroup.GetAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            //db.Groups.Remove(group);
            //await db.SaveChangesAsync();
           if( await _repositoryGroup.DeleteAsync(group))
            {
                return Ok();
            }
            else
            {
               return StatusCode(HttpStatusCode.InternalServerError);
            }

            
        }

        [Authorize(Roles = "Admin")]
        [Route("group/{id:guid}/recipients")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRecipientToGroup([FromUri] Guid idGroup, [FromBody] List<Guid> recipientToAssign)
        {

            Group group = await _repositoryGroup.GetAsync(idGroup);

            // error if groupId doesn't exsist
            if (group == null)
            {
                return NotFound();
            }
            
            foreach(var itemRecipientId in recipientToAssign) {

                Recipient_Group recipientElement = await _repositoryRecipientGroup.GetAsync(itemRecipientId, idGroup);
                    //db.Recipients_Group.FindAsync(itemRecipientId, id);
                if (recipientElement == null) {

                    Recipient_Group elem = new Recipient_Group()
                    {
                        Id_Group = idGroup,
                        Id_Recipient = itemRecipientId,
                        User_Id_Created = new Guid(User.Identity.GetUserId()),
                        Date_Created = DateTime.Now,
                        Active_Flg = true,
                    };
                    _repositoryRecipientGroup.Post(elem);
                    //group.Recipients_Group.Add(elem);
                }
                else
                {
                    recipientElement.Active_Flg = true;
                    recipientElement.User_Id_Modify = new Guid(User.Identity.GetUserId());
                    recipientElement.Date_Modify = DateTime.Now;
                    _repositoryRecipientGroup.Put(recipientElement);
                    //db.Entry(recipientElement).State = EntityState.Modified;
                }
            }


            try
            {
                await _repositoryRecipientGroup.SaveAllAsync();
                //await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", "Failed to add recipients in group  " + ex.Message);
                //throw;
                if (ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("group/{id:guid}/recipients")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveRecipientFromGroup([FromUri] Guid idGroup, [FromBody] List<Guid> recipientToAssign)
        {

            Group group = await _repositoryGroup.GetAsync(idGroup);

            // error if groupId doesn't exsist
            if (group == null)
            {
                return NotFound();
            }

            foreach (var itemRecipientId in recipientToAssign)
            {
                Recipient_Group recipientElement = await _repositoryRecipientGroup.GetAsync(itemRecipientId, idGroup);
                    //db.Recipients_Group.FindAsync(itemRecipientId, id);
                if (recipientElement != null)
                {
                    recipientElement.Active_Flg = false;
                    recipientElement.User_Id_Modify = new Guid(User.Identity.GetUserId());
                    recipientElement.Date_Modify = DateTime.Now;
                    _repositoryRecipientGroup.Put(recipientElement);
                    //db.Entry(recipientElement).State = EntityState.Modified;
                }
            }


            try
            {
                await _repositoryRecipientGroup.SaveAllAsync();
                //await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", "Failed to remove recipients from group " + ex.Message);

                if (ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositoryRecipientGroup.Dispose();
            }
            base.Dispose(disposing);
        }

        #region private methods

        private bool GroupExists(Guid id)
        {
            return _repositoryGroup.GetAll().Count(e => e.Id_Group == id) > 0;
                //db.Groups.Count(e => e.Id_Group == id) > 0;
        }
        #endregion
    }
}