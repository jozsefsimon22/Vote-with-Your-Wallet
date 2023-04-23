using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignaturesController : Controller
    {
        private readonly MyDbContext _context;

        public SignaturesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Signatures
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Signatures.Include(s => s.Cause).Include(s => s.User);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Signatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Signatures == null)
            {
                return NotFound();
            }

            var signatures = await _context.Signatures
                .Include(s => s.Cause)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (signatures == null)
            {
                return NotFound();
            }

            return View(signatures);
        }

        // GET: Signatures/Create
        public IActionResult Create()
        {
           /* ViewData["CauseId"] = new SelectList(_context.Causes, "ID", "CauseName");
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");*/
            return View();
        }

        // POST: Signatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Username,CauseId")] Signatures signatures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signatures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CauseId"] = new SelectList(_context.Causes, "ID", "CauseName", signatures.CauseId);
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", signatures.Username);
            return View(signatures);
        }

        // GET: Signatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Signatures == null)
            {
                return NotFound();
            }

            var signatures = await _context.Signatures.FindAsync(id);
            if (signatures == null)
            {
                return NotFound();
            }
            ViewData["CauseId"] = new SelectList(_context.Causes, "ID", "CauseName", signatures.CauseId);
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", signatures.Username);
            return View(signatures);
        }

        // POST: Signatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,CauseId")] Signatures signatures)
        {
            if (id != signatures.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signatures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignaturesExists(signatures.ID))
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
            ViewData["CauseId"] = new SelectList(_context.Causes, "ID", "CauseName", signatures.CauseId);
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", signatures.Username);
            return View(signatures);
        }

        // GET: Signatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Signatures == null)
            {
                return NotFound();
            }

            var signatures = await _context.Signatures
                .Include(s => s.Cause)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (signatures == null)
            {
                return NotFound();
            }

            return View(signatures);
        }

        // POST: Signatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Signatures == null)
            {
                return Problem("Entity set 'MyDbContext.Signatures'  is null.");
            }
            var signatures = await _context.Signatures.FindAsync(id);
            if (signatures != null)
            {
                _context.Signatures.Remove(signatures);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignaturesExists(int id)
        {
          return (_context.Signatures?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
