using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mtg.Card.Tracker.Controllers
{
    public class UserCardsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}