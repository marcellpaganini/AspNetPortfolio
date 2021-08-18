using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Portfolio.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProject> CategoryProjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasKey(category => category.CategoryId);
            modelBuilder.Entity<Project>()
                .HasKey(project => project.ProjectId);
            modelBuilder.Entity<CategoryProject>()
                .HasKey(categoryProject => new { categoryProject.CategoryId, categoryProject.ProjectId });

            modelBuilder.Entity<Category>()
                .HasMany(category => category.CategoryProjects)
                .WithRequired(categoryProjects => categoryProjects.Category)
                .HasForeignKey(categoryProjects => categoryProjects.CategoryId);
            modelBuilder.Entity<Project>()
                .HasMany(project => project.CategoryProjects)
                .WithRequired(categoryProjects => categoryProjects.Project)
                .HasForeignKey(categoryProjects => categoryProjects.ProjectId);
        }
    }
}