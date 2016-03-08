using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity_WebApi.Data.Entity
{
    public partial class Group 
    {
        
        public Group()
        {
            Recipients_Group = new HashSet<Recipient_Group>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id_Group { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(150)]
        [Display(Name = "Name")]
        public string Name_Group { get; set; }

        [MaxLength(150)]
        [Display(Name = "Description")]
        public string Description_Group { get; set; }

        public virtual Guid User_Id_Created { get; set; }

        public DateTime Date_Created { get; set; }

        public virtual Guid? User_Id_Modify { get; set; }

        public DateTime? Date_Modify { get; set; }

        [Display(Name = "Active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Active_Flg { get; set; }

        public virtual ICollection<Recipient_Group> Recipients_Group { get; set; }

    }
}