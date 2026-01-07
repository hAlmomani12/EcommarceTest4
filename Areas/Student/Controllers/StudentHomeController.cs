using EduSiteHQ.Data;
using EduSiteHQ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EduSiteHQ.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentHomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentHomeController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Dashboard Page
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            // Courses student is enrolled in — include Course and its Instructor
            var myCourses = await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Course)
                    .ThenInclude(c => c.Instructor)
                .Where(e => e.StudentId == user.Id)
                .ToListAsync();

            // Available courses (not enrolled yet) — include Instructor
            var available = await _context.Courses
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Where(c => !_context.Enrollments
                    .Any(e => e.CourseId == c.Id && e.StudentId == user.Id))
                .ToListAsync();

            var model = new StudentDashboardVM
            {
                Student = user,
                MyCourses = myCourses,
                AvailableCourses = available
            };

            return View(model);
        }
    }
}
