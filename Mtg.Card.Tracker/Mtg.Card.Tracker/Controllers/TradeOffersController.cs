using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtg.Card.Tracker.Data;
using Mtg.Card.Tracker.Models;

namespace Mtg.Card.Tracker.Controllers
{
    public class TradeOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TradeOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TradeOffers
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.TradeOffers
                .Include(t => t.CardOffer)
                .Include(t => t.CardRequest)
                .Include(t => t.IdentityUser)
                .Where(t => t.CardRequest.IdentityUserId == userId && t.Accept == null);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PendingTradeOffers()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.TradeOffers
                .Include(t => t.CardOffer)
                .Include(t => t.CardRequest)
                .Include(t => t.IdentityUser)
                .Where(t => t.CardOffer.IdentityUserId == userId && t.Accept ==null);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Accept(int TradeOfferId)
        {
            var tradeOffer = await _context.TradeOffers.FindAsync(TradeOfferId);
            tradeOffer.Accept = true;
            await _context.SaveChangesAsync();
            var userReceiverId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOffererId = tradeOffer.IdentityUserId;

            var offerCard = await _context.MagicCards.FindAsync(tradeOffer.CardOfferId);
            var requestCard = await _context.MagicCards.FindAsync(tradeOffer.CardRequestId);
            offerCard.IdentityUserId = userReceiverId;
            requestCard.IdentityUserId = userOffererId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deny(int TradeOfferId)
        {
            var tradeOffer = await _context.TradeOffers.FindAsync(TradeOfferId);
            tradeOffer.Accept = false;          
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: TradeOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradeOffer = await _context.TradeOffers
                .Include(t => t.CardOffer)
                .Include(t => t.CardRequest)
                .Include(t => t.IdentityUser)
                .FirstOrDefaultAsync(m => m.TradeOfferId == id);
            if (tradeOffer == null)
            {
                return NotFound();
            }

            return View(tradeOffer);
        }

        // GET: TradeOffers/Create
        public IActionResult Create()
        {
            ViewData["CardOfferId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId");
            ViewData["CardRequestId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TradeOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TradeOfferId,IdentityUserId,Accept,CardOfferId,CardRequestId")] TradeOffer tradeOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tradeOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardOfferId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId", tradeOffer.CardOfferId);
            ViewData["CardRequestId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId", tradeOffer.CardRequestId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", tradeOffer.IdentityUserId);
            return View(tradeOffer);
        }

        // GET: TradeOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradeOffer = await _context.TradeOffers.FindAsync(id);
            if (tradeOffer == null)
            {
                return NotFound();
            }
            ViewData["CardOfferId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId", tradeOffer.CardOfferId);
            ViewData["CardRequestId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId", tradeOffer.CardRequestId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", tradeOffer.IdentityUserId);
            return View(tradeOffer);
        }

        // POST: TradeOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TradeOfferId,IdentityUserId,Accept,CardOfferId,CardRequestId")] TradeOffer tradeOffer)
        {
            if (id != tradeOffer.TradeOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tradeOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TradeOfferExists(tradeOffer.TradeOfferId))
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
            ViewData["CardOfferId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId", tradeOffer.CardOfferId);
            ViewData["CardRequestId"] = new SelectList(_context.MagicCards, "MagicCardId", "MagicCardId", tradeOffer.CardRequestId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", tradeOffer.IdentityUserId);
            return View(tradeOffer);
        }

        // GET: TradeOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradeOffer = await _context.TradeOffers
                .Include(t => t.CardOffer)
                .Include(t => t.CardRequest)
                .Include(t => t.IdentityUser)
                .FirstOrDefaultAsync(m => m.TradeOfferId == id);
            if (tradeOffer == null)
            {
                return NotFound();
            }

            return View(tradeOffer);
        }

        // POST: TradeOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tradeOffer = await _context.TradeOffers.FindAsync(id);
            _context.TradeOffers.Remove(tradeOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TradeOfferExists(int id)
        {
            return _context.TradeOffers.Any(e => e.TradeOfferId == id);
        }
    }
}
