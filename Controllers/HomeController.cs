using IntelRobotics.Data;
using IntelRobotics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IntelRobotics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManger = userManger;
            _context = context;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> Index()
        {
            string role = "no";
            var use = _userManger.GetUserName(User);
            var user = _userManger.Users.Where(x => x.UserName == use);
            foreach (var item in user)
            {
                var roles = await _userManger.GetRolesAsync(item);
                if (roles.Count() > 0)
                {
                    role = roles.FirstOrDefault();
                }
            }
            ViewData["role"] = role;
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            string role = "no";
            var use = _userManger.GetUserName(User);
            var user = _userManger.Users.Where(x => x.UserName == use);
            foreach (var item in user)
            {
                var roles = await _userManger.GetRolesAsync(item);
                if (roles.Count() > 0)
                {
                    role = roles.FirstOrDefault();
                }
            }
            ViewData["role"] = role;
            return View();
        }

        public async Task<IActionResult> About()
        {
            string role = "no";
            var use = _userManger.GetUserName(User);
            var user = _userManger.Users.Where(x => x.UserName == use);
            foreach (var item in user)
            {
                var roles = await _userManger.GetRolesAsync(item);
                if (roles.Count() > 0)
                {
                    role = roles.FirstOrDefault();
                }
            }
            ViewData["role"] = role;
            return View();
        }

        public async Task<IActionResult> Engelsk()
        {
            string role = "no";
            var use = _userManger.GetUserName(User);
            var user = _userManger.Users.Where(x => x.UserName == use);
            foreach (var item in user)
            {
                var roles = await _userManger.GetRolesAsync(item);
                if (roles.Count() > 0)
                {
                    role = roles.FirstOrDefault();
                }
            }
            ViewData["role"] = role;
            return View();
        }

        public async Task<IActionResult> Japan()
        {
            string role = "no";
            var use = _userManger.GetUserName(User);
            var user = _userManger.Users.Where(x => x.UserName == use);
            foreach (var item in user)
            {
                var roles = await _userManger.GetRolesAsync(item);
                if (roles.Count() > 0)
                {
                    role = roles.FirstOrDefault();
                }
            }
            ViewData["role"] = role;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> Users()
        {
            string role = "no";
            var usee = _userManger.GetUserName(User);
            var user = _userManger.Users.Where(x => x.UserName == usee);
            foreach (var item in user)
            {
                var roles = await _userManger.GetRolesAsync(item);
                if (roles.Count() > 0)
                {
                    role = roles.FirstOrDefault();
                }
            }
            ViewData["role"] = role;
            List<Users> users1 = new List<Users>();
            var use = _userManger.GetUserName(User);
            var users = _userManger.Users/*.Where(u=>u.UserName != use).ToList()*/;
            foreach (var item in users)
            {

                var roles = await _userManger.GetRolesAsync(item);
                if (roles != null)
                {
                    users1.Add(new Users { Id = item.Id, Username = item.UserName, role=roles});
                }
                else
                {
                    users1.Add(new Users { Id = item.Id, Username = item.UserName, role = roles });
                }
             }
            return View(users1);
        }

        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> add([Bind("Id")] Users userDTO)
        {
            var role = _roleManager.Roles;
            var user = await _userManger.FindByIdAsync(userDTO.Id);
            var roleresult = await _userManger.AddToRoleAsync(user, role.FirstOrDefault().Name);
            return RedirectToAction("Users");

        }

        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> addUser([Bind("Id,role")] Users userDTO)
        {
            var User = await _userManger.FindByIdAsync(userDTO.Id);
            var Usersrole = await _userManger.GetRolesAsync(User);
            foreach (var item in Usersrole)
            {
                var removeroles = await _userManger.RemoveFromRoleAsync(User, item);
            }
            var role = _roleManager.Roles.Where(x=>x.Name==userDTO.role.FirstOrDefault());
            var user = await _userManger.FindByIdAsync(userDTO.Id);
            var roleresult = await _userManger.AddToRoleAsync(user, role.FirstOrDefault().Name);
            return RedirectToAction("Users");

        }

        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> removeRole([Bind("Id,role")] Users userDTO)
        {
            if (userDTO.role.FirstOrDefault()== "SuperAdmin")
            {

                var roles = _roleManager.Roles.Where(x=>x.Name=="Admin");
                var user = await _userManger.FindByIdAsync(userDTO.Id);
                var role = await _userManger.GetRolesAsync(user);
                foreach (var item in role)
                {
                    var removeroles = await _userManger.RemoveFromRoleAsync(user, item);
                }
                var roleresult = await _userManger.AddToRoleAsync(user, roles.FirstOrDefault().Name);
            }
            else
            {
                var user = await _userManger.FindByIdAsync(userDTO.Id);
                var role = await _userManger.GetRolesAsync(user);
                foreach (var item in role)
                {
                    var removeroles = await _userManger.RemoveFromRoleAsync(user, item);
                }
            }
            return RedirectToAction("Users");
        }

        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> delete([Bind("Id")] Users userDTO)
        {

            var user = await _userManger.FindByIdAsync(userDTO.Id);
            var role = await _userManger.GetRolesAsync(user);
            foreach (var item in role)
            {
                var removeroles = await _userManger.RemoveFromRoleAsync(user, item);
            }
            var res = await _userManger.DeleteAsync(user);
            return RedirectToAction("Users");
        }
    }
}