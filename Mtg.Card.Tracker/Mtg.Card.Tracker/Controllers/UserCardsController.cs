using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mtg.Card.Tracker.Data;

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
            var applicationDbContext = _context.Cards.Include(m => m.IdentityUser).
                GroupBy(x => x.IdentityUserId).Select(y => y.First()); 
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? Id)
        {

            var userId = (from s in _context.Cards // grabs the owner of this card
                          where s.MagicCardId == Id
                          select s.IdentityUser.Id).First();
                      
            var applicationDbContext1 = _context.Cards.Include(m => m.IdentityUser)
                .Where(m => m.IdentityUser.Id == userId);
            return View(await applicationDbContext1.ToListAsync()); // returns all the cards users
        }
    }
}