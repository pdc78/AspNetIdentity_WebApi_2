using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity_WebApi.Models
{
    public class GroupCreateModel
    {

        [Required]
        [Index(IsUnique = true)]
        [StringLength(150, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        [MaxLength(150)]
        [Display(Name = "Name")]
        public string NameGroup { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Description")]
        public string DescriptionGroup { get; set; }

        [Required]
        [Display(Name = "Active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool ActiveFlg { get; set; }

    }

    public class GroupUpdateModel : GroupCreateModel
    {
        [Required]
        public Guid IdGroup { get; set; }

    }

    public class GroupResultModel : GroupCreateModel
    {
        [Required]
        public Guid IdGroup { get; set; }

        public string Url { get; set; }


    }

    public class GroupResultWithDetailsModel : GroupResultModel
    {
       public List<RecipientResultModel> Recipients;
    }
}