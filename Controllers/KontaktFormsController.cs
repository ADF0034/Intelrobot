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
    public class KontaktFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KontaktFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KontaktForms
        public async Task<IActionResult> Index()
        {
              return _context.kontaktForms != null ? 
                          View(await _context.kontaktForms.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.kontaktForms'  is null.");
        }

        // GET: KontaktForms/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: KontaktForms/Create
        public IActionResult Create(Guid? id)
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
                    if(data != null)
                    {
                        ViewData["robot"] = data.FirstOrDefault();
                        return View();
                    }
                    return View();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        // POST: KontaktForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompanyName,Email,Regarding,Robotid")] KontaktForm kontaktForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kontaktForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
