using AspNetIdentity_WebApi.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AspNetIdentity_WebApi.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Recipient_Group> RecipientsGroup { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Recipient>()
            //    .HasMany<Group>(s => s.List_of_Groups)
            //    .WithMany(c => c.List_of_Recipient)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("Recipient_Ref_Id_Recipient");
            //        cs.MapRightKey("Group_Ref_Id_Group");
            //        cs.ToTable("Recipient_Group");
            //    });

            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });


        }



    }
}