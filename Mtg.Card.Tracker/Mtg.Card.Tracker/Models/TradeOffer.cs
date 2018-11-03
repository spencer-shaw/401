using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mtg.Card.Tracker.Models
{
    public class TradeOffer
    {
        public int TradeOfferId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }           //Forgien Key to Identity User
        public bool? Accept { get; set; }

        [ForeignKey("CardOffer")]
        public int? CardOfferId { get; set; }
        public MagicCard CardOffer { get; set; }

        [ForeignKey("CardRequest")]
        public int? CardRequestId { get; set; }
        public MagicCard CardRequest { get; set; }



    }
}
