using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mtg.Card.Tracker.Areas.WebServices;
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
            var applicationDbContext = _context.MagicCards.Include(m => m.IdentityUser)
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

            var magicCard = await _context.MagicCards
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
        public async Task<IActionResult> Create([Bind("MagicCardId,Name,Color,Power,Toughness,Description,Type,ManaCost,MultiverseId,IdentityUserId,ImageUrl,CardAmount,Price")] MagicCard magicCard)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var query = (from c in _context.MagicCards
                             where c.ImageUrl == magicCard.ImageUrl && c.IdentityUserId == userId
                             select new { c.CardsAmount, c.MagicCardId, c.Price, c.Color }).FirstOrDefault();
                if(query == null)
                {
                    magicCard.IdentityUserId = userId;
                    magicCard.CardsAmount = magicCard.CardsAmount + 1;
                    _context.Add(magicCard);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    
                    var card = await _context.MagicCards.FindAsync(query.MagicCardId);
                    card.CardsAmount = card.CardsAmount + 1;
                    try
                    {
                        _context.Update(card);
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


                }
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", magicCard.IdentityUserId);
            return View(magicCard);
        }

        public JsonResult CardLookup(string name)
        {
            var scrFall = new ScryFall();
            var card = scrFall.GetCards(name);
            //   var magicCard = new MagicCard();
            //{
            //       magicCard.Name = card.name;
            //       magicCard.ManaCost = card.mana_cost;

            //};
            var description = card.oracle_text;
            var image = card.image_uris.small;
            return Json(card);
        }

        // GET: MagicCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magicCard = await _context.MagicCards.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("MagicCardId,Name,Color,Power,Toughness,Description,Type,ManaCost,MultiverseId,IdentityUserId,ImageUrl,CardsAmount,Price")] MagicCard magicCard)
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

            var magicCard = await _context.MagicCards
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
            var magicCard = await _context.MagicCards.FindAsync(id);
            _context.MagicCards.Remove(magicCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagicCardExists(int id)
        {
            return _context.MagicCards.Any(e => e.MagicCardId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sort(string sortType)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.MagicCards.Include(m => m.IdentityUser)
                .Where(m => m.IdentityUser.Id == userId);
            if (sortType != null || sortType != "")
            {
                if (sortType.Equals("Name"))
                {
                    applicationDbContext = _context.MagicCards.Include(m => m.IdentityUser)
                   .Where(m => m.IdentityUser.Id == userId).OrderBy(o => o.Name);
                }
                else if (sortType.Equals("Color"))
                {
                    applicationDbContext = _context.MagicCards.Include(m => m.IdentityUser)
                  .Where(m => m.IdentityUser.Id == userId).OrderBy(o => o.Color);
                }
                else if (sortType.Equals("Price"))
                {
                    applicationDbContext = _context.MagicCards.Include(m => m.IdentityUser)
                   .Where(m => m.IdentityUser.Id == userId).OrderBy(o => o.Price);
                }
            }
           
            return View("Index", await applicationDbContext.ToListAsync());
        }
    }
}
