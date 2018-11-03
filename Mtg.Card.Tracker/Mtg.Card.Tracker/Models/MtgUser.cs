using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtg.Card.Tracker.Models
{
    public class MtgUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        public ICollection<MagicCard> MagicCard { get; set; }
        public ICollection<TradeOffer> TradeOffer { get; set; }
    }
}
