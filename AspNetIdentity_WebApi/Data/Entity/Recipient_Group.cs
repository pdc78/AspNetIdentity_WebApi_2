using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity_WebApi.Data.Entity
{
    
    public  class Recipient_Group 
    {
        // Foreign Key
        [Required]
        //[InverseProperty("Id_Recipient")]
        //[ForeignKey("Recipient_Ref_Id_Recipient")]
        [Key, Column(Order = 0)]
        public Guid Id_Recipient { get; set; }

        // Foreign Key
        [Required]
        //[InverseProperty("Id_Group")]
        //[ForeignKey("Group_Ref_Id_Group")]
        [Key, Column(Order = 1)]
        public Guid Id_Group { get; set; }

        public virtual Guid User_Id_Created { get; set; }

        public DateTime Date_Created { get; set; }

        public virtual Guid? User_Id_Modify { get; set; }

        public DateTime? Date_Modify { get; set; }

        [Display(Name = "Active")]
        [DefaultValue(true)]
        public bool Active_Flg { get; set; }

        // Navigation property
        public virtual Group Group { get; set; }
        // Navigation property
        public virtual Recipient Recipient { get; set; }
    }
}