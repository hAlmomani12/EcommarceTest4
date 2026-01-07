using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSiteHQ.Models
{
    public class CourseMaterial
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; } = "";

        // PDF/IMAGE/FILE/VIDEO_LINK
        [Required, StringLength(30)]
        public string Type { get; set; } = "FILE";

        // لو ملف: نخزن المسار مثل "/uploads/....pdf"
        // لو فيديو: نخزن الرابط مثل "https://youtube.com/...."
        [Required]
        public string UrlOrPath { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
