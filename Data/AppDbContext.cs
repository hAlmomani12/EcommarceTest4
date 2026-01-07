using EduSiteHQ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduSiteHQ.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            OnDelete(DeleteBehavior.Restrict);

        }

        private void OnDelete(DeleteBehavior restrict)
        {
            throw new NotImplementedException();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }

    }
}
