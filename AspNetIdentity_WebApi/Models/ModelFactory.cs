using AspNetIdentity_WebApi.Data.Entity;
using AspNetIdentity_WebApi.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace AspNetIdentity_WebApi.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Sender = appUser.Sender,
                ActiveFlg = appUser.Active_Flg,
                DateCreated = appUser.Date_Created,
                UserIdCreated = appUser.User_Id_Created,
                DateModify = appUser.Date_Modify,
                UserIdModify = appUser.User_Id_Modify,
                //Level = appUser.Level,
                //JoinDate = appUser.JoinDate,
                Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }
        
        public RecipientResultModel Create(Recipient recip)
        {
            return new RecipientResultModel
            {
                Url = _UrlHelper.Link("GetRecipient", new { id = recip.Id_Recipient }),
                IdRecipient=recip.Id_Recipient,
                ActiveFlg=recip.Active_Flg,
                Address = recip.Address,
                Birthday = recip.Birthday,
                City = recip.City,
                Country = recip.Country,
                Email = recip.Email,
                FirstName = recip.FirstName,
                HomePhoneNumber = recip.HomePhoneNumber,
                MobileNumber = recip.MobileNumber,
                OfficePhoneNumber = recip.OfficePhoneNumber,
                Province = recip.Province,
                PostalCode = recip.PostalCode,
                SecondName = recip.SecondName,
                Gender = recip.Gender
            };
        }

        public GroupResultModel Create(Group group)
        {
            return new GroupResultModel
            {
                Url = _UrlHelper.Link("GetGroup", new { id = group.Id_Group }),
                IdGroup = group.Id_Group,
                ActiveFlg = group.Active_Flg,
                DescriptionGroup = group.Description_Group,
                NameGroup = group.Name_Group
            };

        }

        public GroupResultWithDetailsModel Create(Group group,IQueryable<Recipient> recipients)
        {
            GroupResultWithDetailsModel localResut = new GroupResultWithDetailsModel
            {
                Url = _UrlHelper.Link("GetGroup", new { id = group.Id_Group }),
                IdGroup = group.Id_Group,
                ActiveFlg = group.Active_Flg,
                DescriptionGroup = group.Description_Group,
                NameGroup = group.Name_Group
            };

            List<RecipientResultModel> lstRecip = new List<RecipientResultModel>();

            foreach (Recipient elem in recipients)
            {
                RecipientResultModel element = Create(elem);
                lstRecip.Add(element);
            }

            localResut.Recipients = lstRecip;

            return localResut;
        }



        public RoleReturnModel Create(IdentityRole appRole)
        {

            return new RoleReturnModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };
        }
    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool ActiveFlg { get; set; }
        public int? UserIdCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserIdModify { get; set; }
        public DateTime? DateModify { get; set; }
        public string Sender { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }


}