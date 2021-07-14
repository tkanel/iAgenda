using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iAgenda.Data;
using iAgenda.Models;

namespace iAgenda.Controllers
{
    public class BranchOfficesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchOfficesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BranchOffices
        public async Task<IActionResult> Index()
        {
            return View(await _context.BranchOffices.ToListAsync());
        }

        // GET: BranchOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOffice = await _context.BranchOffices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branchOffice == null)
            {
                return NotFound();
            }

            return View(branchOffice);
        }

        // GET: BranchOffices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BranchOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BranchOffice branchOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branchOffice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branchOffice);
        }

        // GET: BranchOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOffice = await _context.BranchOffices.FindAsync(id);
            if (branchOffice == null)
            {
                return NotFound();
            }
            return View(branchOffice);
        }

        // POST: BranchOffices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BranchOffice branchOffice)
        {
            if (id != branchOffice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branchOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchOfficeExists(branchOffice.Id))
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
            return View(branchOffice);
        }

        // GET: BranchOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOffice = await _context.BranchOffices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branchOffice == null)
            {
                return NotFound();
            }

            return View(branchOffice);
        }

        // POST: BranchOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branchOffice = await _context.BranchOffices.FindAsync(id);
            _context.BranchOffices.Remove(branchOffice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchOfficeExists(int id)
        {
            return _context.BranchOffices.Any(e => e.Id == id);
        }
    }
}
