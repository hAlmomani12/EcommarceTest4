using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSiteHQ.Models
{
    public class ApplicationUser : IdentityUser

    {
        public string FullName { get; set; } = ""; 
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [NotMapped]   // يعني EF Core ما رح ينشئ عمود في DB
        public string? RoleName { get; set; }

    }
}
