using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelRobotics.Data;
using IntelRobotics.Models;

namespace IntelRobotics.Controllers
{
    public class RobotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RobotsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Robots
        public async Task<IActionResult> Index()
        {
              return _context.Robots != null ? 
                          View(await _context.Robots.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Robots'  is null.");
        }

        // GET: Robots/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Robots == null)
            {
                return NotFound();
            }

            var robot = await _context.Robots
                .FirstOrDefaultAsync(m => m.Robotid == id);
            if (robot == null)
            {
                return NotFound();
            }

            return View(robot);
        }

        // GET: Robots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Robots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,available,Price,ImageFile,LigtingCapacity,Weight,Footprint,Radius,Parts,Edit")] Robot robot)
        {
            //Save Image to wwwroot/image
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(robot.ImageFile.FileName);
                string extension = Path.GetExtension(robot.ImageFile.FileName);
                robot.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await robot.ImageFile.CopyToAsync(fileStream);
                }
                //Insert record
                robot.Robotid = Guid.NewGuid();
                _context.Add(robot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(robot);
        }

        // GET: Robots/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Robots == null)
            {
                return NotFound();
            }

            var robot = await _context.Robots.FindAsync(id);
            if (robot == null)
            {
                return NotFound();
            }
            return View(robot);
        }

        // POST: Robots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,available,Price,ImageName,LigtingCapacity,Weight,Footprint,Radius,Parts")] Robot robot)
        {
            if (id != robot.Robotid)
            {
                return NotFound();
            }
            DateTime date = DateTime.Now;
            robot.Edit=date;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(robot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RobotExists(robot.Robotid))
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
            return View(robot);
        }

        // GET: Robots/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Robots == null)
            {
                return NotFound();
            }

            var robot = await _context.Robots
                .FirstOrDefaultAsync(m => m.Robotid == id);
            if (robot == null)
            {
                return NotFound();
            }

            return View(robot);
        }

        // POST: Robots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Robots == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Robots'  is null.");
            }
            var robot = await _context.Robots.FindAsync(id);
            var imagepath = Path.Combine(_hostEnvironment.WebRootPath,"image",robot.ImageName);
            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            if (robot != null)
            {
                _context.Robots.Remove(robot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RobotExists(Guid id)
        {
          return (_context.Robots?.Any(e => e.Robotid == id)).GetValueOrDefault();
        }
    }
}
