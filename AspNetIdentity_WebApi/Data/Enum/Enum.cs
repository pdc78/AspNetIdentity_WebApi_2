using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity_WebApi.Data.Enum
{
    public enum Gender
    {
        [Display(Name = "M")]
        Male = 0,
        [Display(Name = "F")]
        Female = 1
    }

}