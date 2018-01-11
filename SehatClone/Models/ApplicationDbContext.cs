using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SehatClone.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<CenterType> CenterTypes { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<CenterServices> CenterServices { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}