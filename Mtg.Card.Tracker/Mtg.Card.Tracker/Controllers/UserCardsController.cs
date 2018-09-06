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

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Cards.Include(m => m.IdentityUser);            
            return View(await applicationDbContext.ToListAsync());
        }
    }
}