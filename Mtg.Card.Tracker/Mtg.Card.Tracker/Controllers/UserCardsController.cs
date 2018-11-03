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
    public class UserCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {         
            var applicationDbContext = _context.MagicCards.Include(m => m.IdentityUser).
                GroupBy(x => x.IdentityUserId).Select(y => y.First()); 
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? Id)
        {

            var userId = (from s in _context.MagicCards // grabs the owner of this card
                          where s.MagicCardId == Id
                          select s.IdentityUser.Id).First();

            var applicationDbContext1 = _context.MagicCards.Include(m => m.IdentityUser)
                .Where(m => m.IdentityUser.Id == userId);
            return View(await applicationDbContext1.ToListAsync()); // returns all the cards users
        }

        //TODO This logic is right. Just make the view an Ajax call
        public async Task<IActionResult> RequestTrade(int requestId, int offerId)
        {
            if (requestId == null)
            {
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var magicCard = await _context.MagicCards
                .Include(m => m.IdentityUser)
                .FirstOrDefaultAsync(m => m.MagicCardId == requestId);
            if (magicCard == null)
            {
                return NotFound();
            }
            var query = new TradeOffer()
            {
                IdentityUserId = userId,
                Accept = null,
                CardRequestId = magicCard.MagicCardId,
                CardOfferId = offerId
            };
                                             
            _context.Add(query);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> TradeOffer(int? id)
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

    }
}