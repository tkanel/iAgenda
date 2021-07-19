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
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Persons.Include(p => p.BranchOffice).Include(p => p.Department).OrderBy(p=>p.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.BranchOffice)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description");
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,InternalPhone,Mobile1,Mobile2,FourDigitsCode,Notes,Email,DepartmentId,BranchOfficeId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name", person.BranchOfficeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", person.DepartmentId);
            return View(person);
        }

        // GET: Persons/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name", person.BranchOfficeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", person.DepartmentId);
            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InternalPhone,Mobile1,Mobile2,FourDigitsCode,Notes,Email,DepartmentId,BranchOfficeId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            ViewData["BranchOfficeId"] = new SelectList(_context.BranchOffices, "Id", "Name", person.BranchOfficeId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Description", person.DepartmentId);
            return View(person);
        }

        // GET: Persons/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.BranchOffice)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
