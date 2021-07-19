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
    public class ExternalContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExternalContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExternalContacts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExternalContacts.Include(e => e.Companies).OrderBy(e=>e.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExternalContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var externalContact = await _context.ExternalContacts
                .Include(e => e.Companies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (externalContact == null)
            {
                return NotFound();
            }

            return View(externalContact);
        }

        // GET: ExternalContacts/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName");
            return View();
        }

        // POST: ExternalContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Mobile1,Mobile2,Notes,Email,CompanyId")] ExternalContact externalContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(externalContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", externalContact.CompanyId);
            return View(externalContact);
        }

        // GET: ExternalContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var externalContact = await _context.ExternalContacts.FindAsync(id);
            if (externalContact == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", externalContact.CompanyId);
            return View(externalContact);
        }

        // POST: ExternalContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Mobile1,Mobile2,Notes,Email,CompanyId")] ExternalContact externalContact)
        {
            if (id != externalContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(externalContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExternalContactExists(externalContact.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", externalContact.CompanyId);
            return View(externalContact);
        }

        // GET: ExternalContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var externalContact = await _context.ExternalContacts
                .Include(e => e.Companies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (externalContact == null)
            {
                return NotFound();
            }

            return View(externalContact);
        }

        // POST: ExternalContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var externalContact = await _context.ExternalContacts.FindAsync(id);
            _context.ExternalContacts.Remove(externalContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExternalContactExists(int id)
        {
            return _context.ExternalContacts.Any(e => e.Id == id);
        }
    }
}
