using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtg.Card.Tracker.Models
{
    public class MagicCard
    {
        public int MagicCardId { get; set; }
        public string Color { get; set; }
        public int Power { get; set; }
        public int Toughness { get; set; }
        public string Description { get; set; }
        public string IdentityUserId { get; set; }           //Forgien Key to Identity User
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
