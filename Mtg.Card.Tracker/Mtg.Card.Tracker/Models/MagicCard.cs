using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mtg.Card.Tracker.Models
{
    public class MagicCard
    {
        public int MagicCardId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? ManaCost { get; set; }
        public int? MultiverseId { get; set; }
        public string IdentityUserId { get; set; }           //Forgien Key to Identity User
        public string ImageUrl { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public int CardsAmount { get; set; }  // Amount of certain card
      
        [InverseProperty("CardOffer")]
        public TradeOffer CardOffers { get; set; }
        [InverseProperty("CardRequest")]
        public TradeOffer CardRequests { get; set; }


    }
}
