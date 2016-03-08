using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity_WebApi.Models
{
    public class RecipientCreateModel
    {
        public string Url { get; set; }

        [Required(ErrorMessage = "The First Name address is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Second Name address is required")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "The Mobile is required")]
        [Display(Name = "Mobile")]
        public string MobileNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Office Number")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public string OfficePhoneNumber { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public string HomePhoneNumber { get; set; }

        [Display(Name = "Active")]
        public bool ActiveFlg { get; set; }
    }

    public class RecipientUpdateModel : RecipientCreateModel
    {
        public virtual Guid? UserIdModify { get; set; }
        public DateTime? DateModify { get; set; }
    }

    public class RecipientResultModel
    {
        public Guid IdRecipient { get; set; }

        public string Url { get; set; }

        [Required(ErrorMessage = "The First Name address is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Second Name address is required")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "The Mobile is required")]
        [Display(Name = "Mobile")]
        public string MobileNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Office Number")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public string OfficePhoneNumber { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public string HomePhoneNumber { get; set; }

        [Display(Name = "Active")]
        public bool ActiveFlg { get; set; }

        public  Data.Enum.Gender Gender;
    }

}