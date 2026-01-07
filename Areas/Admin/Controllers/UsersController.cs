using EduSiteHQ.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduSiteHQ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleName = string.Join(", ", roles);
            }

            Console.WriteLine("*******    "  +  users + "   *********");

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = new List<string> { "Admin", "Instructor", "Student" };

            ViewBag.UserId = id;
            ViewBag.UserEmail = user.Email;
            ViewBag.CurrentRole = userRoles.FirstOrDefault();
            ViewBag.AllRoles = allRoles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string id, string newRole)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, newRole);

            TempData["Success"] = $"Role for {user.Email} changed to {newRole}";
            return RedirectToAction("Index");
        }


    }
}
