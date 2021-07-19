using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iAgenda.Data;
using iAgenda.Models;
using Microsoft.AspNetCore.Authorization;

namespace iAgenda.Controllers
{
    public class DriversController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DriversController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drivers
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var applicationDbContext = _context.Drivers.Include(d => d.BranchOffice).Include(d => d.Department).OrderBy(d=>d.Name);

            var drivers = from p in applicationDbContext
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {

                drivers = drivers.Where(p => p.Name.Contains(searchString) || p.Phone.Contains(searchString) || p.Mobile1.Contains(searchString) || p.Mobile2.Contains(searchString) || p.FourDigitsCode.Contains(searchString) || p.Email.Contains(searchString) || p.Department.Description.Contains(searchString) || p.BranchOffice.Name.Contains(searchString) || p.Notes.Contains(searchString));

            }


            ViewBag.DriversCount = drivers.Count();


            return View(await drivers.ToListAsync());
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(d => d.BranchOffice)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description");
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Mobile1,Mobile2,FourDigitsCode,TrackNr,Notes,Email,DepartmentId,BranchOfficeId")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name", driver.BranchOfficeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", driver.DepartmentId);
            return View(driver);
        }

        // GET: Drivers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name", driver.BranchOfficeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", driver.DepartmentId);
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Mobile1,Mobile2,FourDigitsCode,TrackNr,Notes,Email,DepartmentId,BranchOfficeId")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
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
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name", driver.BranchOfficeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", driver.DepartmentId);
            return View(driver);
        }

        // GET: Drivers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(d => d.BranchOffice)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}
