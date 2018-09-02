using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtg.Card.Tracker.Data;
using Mtg.Card.Tracker.Models;

namespace Mtg.Card.Tracker.Controllers
{
    [Authorize]
    public class MagicCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MagicCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MagicCards
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Cards.Include(m => m.IdentityUser)
                .Where(m => m.IdentityUser.Id == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MagicCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicCard = await _context.Cards
                .Include(m => m.IdentityUser)
                .FirstOrDefaultAsync(m => m.MagicCardId == id);
            if (magicCard == null)
            {
                return NotFound();
            }

            return View(magicCard);
        }

        // GET: MagicCards/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: MagicCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MagicCardId,Name,Color,Power,Toughness,Description,Type,ManaCost,MultiverseId,IdentityUserId")] MagicCard magicCard)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                magicCard.IdentityUserId = userId;
                _context.Add(magicCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", magicCard.IdentityUserId);
            return View(magicCard);
        }

        // GET: MagicCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicCard = await _context.Cards.FindAsync(id);
            if (magicCard == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", magicCard.IdentityUserId);
            return View(magicCard);
        }

        // POST: MagicCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MagicCardId,Name,Color,Power,Toughness,Description,Type,ManaCost,MultiverseId,IdentityUserId")] MagicCard magicCard)
        {
            if (id != magicCard.MagicCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magicCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagicCardExists(magicCard.MagicCardId))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", magicCard.IdentityUserId);
            return View(magicCard);
        }

        // GET: MagicCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicCard = await _context.Cards
                .Include(m => m.IdentityUser)
                .FirstOrDefaultAsync(m => m.MagicCardId == id);
            if (magicCard == null)
            {
                return NotFound();
            }

            return View(magicCard);
        }

        // POST: MagicCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magicCard = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(magicCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagicCardExists(int id)
        {
            return _context.Cards.Any(e => e.MagicCardId == id);
        }
    }
}
