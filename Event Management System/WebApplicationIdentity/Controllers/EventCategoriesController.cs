using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationIdentity.Data;
using WebApplicationIdentity.Models;

namespace WebApplicationIdentity.Controllers
{
    public class EventCategoriesController : Controller
    {
        private readonly MyDbContext _context;

        public EventCategoriesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: EventCategories
        public async Task<IActionResult> Index()
        {
              return _context.EventCategory != null ? 
                          View(await _context.EventCategory.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.EventCategory'  is null.");
        }

        // GET: EventCategories/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EventCategory == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategory
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventCategory == null)
            {
                return NotFound();
            }

            return View(eventCategory);
        }

        // GET: EventCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,Description")] EventCategory eventCategory)
        {
                _context.Add(eventCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            return View(eventCategory);
        }

        // GET: EventCategories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EventCategory == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategory.FindAsync(id);
            if (eventCategory == null)
            {
                return NotFound();
            }
            return View(eventCategory);
        }

        // POST: EventCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EventId,Name,Description")] EventCategory eventCategory)
        {
            if (id != eventCategory.EventId)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(eventCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCategoryExists(eventCategory.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(eventCategory);
        }

        // GET: EventCategories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EventCategory == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategory
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventCategory == null)
            {
                return NotFound();
            }

            return View(eventCategory);
        }

        // POST: EventCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EventCategory == null)
            {
                return Problem("Entity set 'MyDbContext.EventCategory'  is null.");
            }
            var eventCategory = await _context.EventCategory.FindAsync(id);
            if (eventCategory != null)
            {
                _context.EventCategory.Remove(eventCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventCategoryExists(string id)
        {
          return (_context.EventCategory?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
