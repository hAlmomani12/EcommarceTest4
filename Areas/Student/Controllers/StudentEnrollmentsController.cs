using EduSiteHQ.Data;
using EduSiteHQ.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduSiteHQ.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentEnrollmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentEnrollmentsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Enroll Action
        public async Task<IActionResult> Enroll(int courseId)
        {
            var student = await _userManager.GetUserAsync(User);

            if (student == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            // Check if already enrolled
            bool exists = await _context.Enrollments
                .AnyAsync(e => e.CourseId == courseId && e.StudentId == student.Id);

            if (exists)
            {
                TempData["Error"] = "You are already enrolled in this course!";
                return RedirectToAction("Index", "StudentHome", new { area = "Student" });
            }

            // Create new enrollment
            var enroll = new Enrollment
            {
                CourseId = courseId,
                StudentId = student.Id,
                EnrollmentDate = DateTime.Now
            };

            _context.Enrollments.Add(enroll);
            await _context.SaveChangesAsync();

            TempData["SuccessEnrollment"] = "Enrollment successful!";
            return RedirectToAction("Index", "StudentHome", new { area = "Student" });
        }
    }
}
