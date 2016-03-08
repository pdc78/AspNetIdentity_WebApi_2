using AspNetIdentity_WebApi.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetIdentity_WebApi.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        //[Required]
        [MaxLength(100)]
        [Required(ErrorMessage = "The First Name address is required")]
        public string FirstName { get; set; }

        //[Required]
        [MaxLength(100)]
        [Required(ErrorMessage = "The Second Name address is required")]
        public string LastName { get; set; }

        [Required]
        public byte Level { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Active_Flg { get; set; }
        
        public int? User_Id_Created { get; set; }

        //[Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime? Date_Created { get; set; }

        public int? User_Id_Modify { get; set; }

        //[Display(Name = "Date Modified")]
        [DataType(DataType.DateTime)]
        public DateTime? Date_Modify { get; set; }

        public string Sender { get; set; }

        public ICollection<Recipient> ListRecipients;
        public ICollection<Group> ListGroups;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}