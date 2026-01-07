using System.Collections.Generic;

namespace EduSiteHQ.Models
{
    public class StudentDashboardVM
    {
        public ApplicationUser Student { get; set; }
        public List<Enrollment> MyCourses { get; set; }
        public List<Course> AvailableCourses { get; set; }
    }
}
