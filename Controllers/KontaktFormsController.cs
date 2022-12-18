using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelRobotics.Data;
using IntelRobotics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IntelRobotics.Controllers
{
    public class KontaktFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public KontaktFormsController(ApplicationDbContext context, UserManager<IdentityUser> userManger, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManger = userManger;
            _roleManager = roleManager;
        }





        // GET: KontaktForms
        public async Task<IActionResult> Index()
        {
            List<KontaktForm> kontaktForms = new List<KontaktForm>();
            var kontaktforms = await _context.kontaktForms.ToListAsync();
            var KontaktTorobot = await _context.kontaktFormToRobots.ToListAsync();
            foreach (var item in kontaktforms)
            {
                var KTR = KontaktTorobot.Where(x => x.KontaktFormId == item.Id);
                if (KTR.Count()>0)
                {
                    var robot = _context.Robots.Where(x => x.Robotid == KTR.FirstOrDefault().RobotId).AsNoTracking();
                    KontaktForm kontaktForm = new KontaktForm()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        CompanyName = item.CompanyName,
                        Name = item.Name,
                        Regarding = item.Regarding,
                        RequestDate = item.RequestDate,
                        RB = robot.FirstOrDefault().Name,
                        RBIMAGE = robot.FirstOrDefault().ImageName,
                    };
                    kontaktForms.Add(kontaktForm);
                }
                else
                {
                    kontaktForms.Add(item);
                }
            }
            return View(kontaktForms);
            
        }

        // GET: KontaktForms/Details/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.kontaktForms == null)
            {
                return NotFound();
            }
            else
            {
                List<KontaktForm> kontaktForms = new List<KontaktForm>();
                var kontaktforms = await _context.kontaktForms.Where(x=>x.Id==id).ToListAsync();
                var KontaktTorobot = await _context.kontaktFormToRobots.ToListAsync();
                foreach (var item in kontaktforms)
                {
                    var KTR = KontaktTorobot.Where(x => x.KontaktFormId == item.Id);
                    if (KTR.Count() > 0)
                    {

                        var robot = _context.Robots.Where(x => x.Robotid == KTR.FirstOrDefault().RobotId).AsNoTracking();
                        KontaktForm kontaktForm = new KontaktForm()
                        {
                            Id = item.Id,
                            Email = item.Email,
                            CompanyName = item.CompanyName,
                            Name = item.Name,
                            Regarding = item.Regarding,
                            RequestDate = item.RequestDate,
                            RB = robot.FirstOrDefault().Name,
                            RBIMAGE = robot.FirstOrDefault().ImageName,

                        };
                        kontaktForms.Add(kontaktForm);
                    }
                    else
                    {
                        kontaktForms.Add(item);
                    }
                }
                ViewData["data"] = kontaktForms.FirstOrDefault() as KontaktForm;
                return View();
            }
            return NotFound();
        }

        // GET: KontaktForms/Create

        public async Task<IActionResult> Create(Guid? id)
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
            if (role!="no")
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    try
                    {
                        var data = _context.Robots.Where(x => x.Robotid == id);
                        if (data != null)
                        {
                            ViewData["robot"] = data.FirstOrDefault();
                            ViewData["Name"] = data.FirstOrDefault().Name;
                            return View();
                        }
                        return View();
                    }
                    catch (Exception)
                    {
                        return Problem();
                    }
                }
            }
        }

        // POST: KontaktForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CompanyName,Email,Regarding,Robotid")] KontaktForm kontaktForm)
        {

            if (ModelState.IsValid)
            {
                Guid guid = Guid.Parse("1f46ed45-f49c-4dca-83f6-d956c9344c65");
                if (kontaktForm.Robotid ==guid)
                {
                    DateTime dateTime = DateTime.Now;
                    kontaktForm.RequestDate = dateTime;
                    _context.Add(kontaktForm);
                    await _context.SaveChangesAsync();
                    KontaktFormToRobot robot = new KontaktFormToRobot();
                    robot.KontaktFormId = kontaktForm.Id;
                    Guid gu = kontaktForm.Robotid.Value;
                    robot.RobotId = gu;
                    _context.kontaktFormToRobots.Add(robot);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    DateTime dateTime = DateTime.Now;
                    kontaktForm.RequestDate = dateTime;
                    _context.Add(kontaktForm);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index","Home");
            }
            return View(kontaktForm);
        }

        // GET: KontaktForms/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.kontaktForms == null)
            {
                return NotFound();
            }

            var kontaktForm = await _context.kontaktForms.FindAsync(id);
            if (kontaktForm == null)
            {
                return NotFound();
            }
            return View(kontaktForm);
        }

        // POST: KontaktForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompanyName,Email,Regarding,RequestDate")] KontaktForm kontaktForm)
        {
            if (id != kontaktForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kontaktForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontaktFormExists(kontaktForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kontaktForm);
        }

        [Authorize(Policy = "Admin")]
        // GET: KontaktForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.kontaktForms == null)
            {
                return NotFound();
            }

            var kontaktForm = await _context.kontaktForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kontaktForm == null)
            {
                return NotFound();
            }

            return View(kontaktForm);
        }

        // POST: KontaktForms/Delete/5
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.kontaktForms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.kontaktForms'  is null.");
            }
            var kontaktForm = await _context.kontaktForms.FindAsync(id);
            if (kontaktForm != null)
            {
                _context.kontaktForms.Remove(kontaktForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontaktFormExists(int id)
        {
          return (_context.kontaktForms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        //[Authorize]
        //public async Task<string> EditUserRolsAsync()
        //{
        //    IdentityRole identityRole = new IdentityRole
        //    {
        //        Name = "SuperAdmin",
        //    };
        //    IdentityResult result = await _roleManager.CreateAsync(identityRole);
        //    if (result.Succeeded)
        //    {
        //        return "Yes";
        //    }
        //    return "no";
        //}
    }
}
