using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSiteHQ.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Display(Name = "Created Date")]

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // ✅ علاقة مع جدول Users (AspNetUsers)
        [Required]
        [Display(Name = "Instructor")]
        public string InstructorId { get; set; } = string.Empty; 

        [ForeignKey("InstructorId")]
        public ApplicationUser? Instructor { get; set; }

    }
}
