using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetIdentity_WebApi.Data.Enum;

namespace AspNetIdentity_WebApi.Data.Entity
{
    public partial class Recipient 
    {
        public Recipient()
        {
            Recipients_Group = new HashSet<Recipient_Group>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id_Recipient { get; set; }

        [Required(ErrorMessage = "The First Name address is required")]
        [Display(Name = "First Name")]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Second Name address is required")]
        [Display(Name = "Second Name")]
        [MaxLength(150)]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "The Mobile is required")]
        [Display(Name = "Mobile")]
        [MaxLength(10)]
        [Index(IsUnique = true)]
        public string MobileNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(300)]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(150)]
        public string City { get; set; }

        [MaxLength(15)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [MaxLength(150)]
        public string Province { get; set; }

        [MaxLength(150)]
        public string Country { get; set; }

        public Gender Gender { get; set; }

        [NotMapped]
        public int? Age {
            set {if (Birthday == null) Age = null; Age = GetAge(Convert.ToDateTime(Birthday)); }
            get { if (Birthday == null) return null; return GetAge(Convert.ToDateTime(Birthday)); }
        }

        //[DataType(DataType.Date)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ? Birthday { get; set; }

        [Display(Name = "Office Number")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        [MaxLength(15)]
        public string OfficePhoneNumber { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        [MaxLength(15)]
        public string HomePhoneNumber { get; set; }

        public virtual Guid User_Id_Created { get; set; }

        public DateTime Date_Created { get; set; }

        public virtual Guid? User_Id_Modify { get; set; }

        public DateTime? Date_Modify { get; set; }

        [Display(Name = "Active")]
        [DefaultValue(true)]
        public bool Active_Flg { get; set; }

        public virtual ICollection<Recipient_Group> Recipients_Group { get; set; }
        

        public static int GetAge(DateTime birthDate)
        {
            DateTime n = DateTime.Now; // To avoid a race condition around midnight
            int age = n.Year - birthDate.Year;

            if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
                age--;

            return age;
        }
    }
}