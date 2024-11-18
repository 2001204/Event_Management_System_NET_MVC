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
    public class EventDetailsController : Controller
    {
        private readonly MyDbContext _context;

        public EventDetailsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: EventDetails
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.EventDetail.Include(e => e.User);
            return View(await myDbContext.ToListAsync());
        }

        // GET: EventDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EventDetail == null)
            {
                return NotFound();
            }

            var eventDetail = await _context.EventDetail
                
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventDetailId == id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            return View(eventDetail);
        }

        // GET: EventDetails/Create
        public IActionResult Create()
        {
                      ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: EventDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventDetailId,Title,Description,StartDate,EndDate,Venue,UserId")] EventDetail eventDetail)
        {
           
                _context.Add(eventDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", eventDetail.UserId);
            return View(eventDetail);
        }

        // GET: EventDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EventDetail == null)
            {
                return NotFound();
            }

            var eventDetail = await _context.EventDetail.FindAsync(id);
            if (eventDetail == null)
            {
                return NotFound();
            }
             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", eventDetail.UserId);
            return View(eventDetail);
        }

        // POST: EventDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EventDetailId,Title,Description,StartDate,EndDate,VenueUserId")] EventDetail eventDetail)
        {
            if (id != eventDetail.EventDetailId)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(eventDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventDetailExists(eventDetail.EventDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", eventDetail.UserId);
            return View(eventDetail);
        }

        // GET: EventDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EventDetail == null)
            {
                return NotFound();
            }

            var eventDetail = await _context.EventDetail
                
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventDetailId == id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            return View(eventDetail);
        }

        // POST: EventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EventDetail == null)
            {
                return Problem("Entity set 'MyDbContext.EventDetail'  is null.");
            }
            var eventDetail = await _context.EventDetail.FindAsync(id);
            if (eventDetail != null)
            {
                _context.EventDetail.Remove(eventDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventDetailExists(string id)
        {
          return (_context.EventDetail?.Any(e => e.EventDetailId == id)).GetValueOrDefault();
        }
    }
}
