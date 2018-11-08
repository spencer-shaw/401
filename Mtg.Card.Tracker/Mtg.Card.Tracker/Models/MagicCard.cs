using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mtg.Card.Tracker.Models
{
    public class MagicCard
    {
        public int MagicCardId { get; set; }
        [Display(Name = "Card Name")]
        public string Name { get; set; }
        public string Color { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? ManaCost { get; set; }
        [Display(Name = "Multiverse Id")]
        public int? MultiverseId { get; set; }
        public string IdentityUserId { get; set; }           //Forgien Key to Identity User
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        [Display(Name = "Quantity")]
        public int CardsAmount { get; set; }  // Amount of certain card
        [Display(Name = "Price USD")]
        public double Price { get; set; }
      
        [InverseProperty("CardOffer")]
        public TradeOffer CardOffers { get; set; }
        [InverseProperty("CardRequest")]
        public TradeOffer CardRequests { get; set; }


    }
}
