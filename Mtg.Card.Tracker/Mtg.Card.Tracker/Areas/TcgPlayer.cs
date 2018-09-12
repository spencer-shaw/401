using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Scryfall.API;


namespace Mtg.Card.Tracker.Areas
{
    public class TcgPlayer
    {
		public static string getNameFuzzy(string cardName)
		{
			// Instiantiate new Scryfall client
			var scryfall = new ScryfallClient();

			//Use fuzzy api name to retrieve exact name
			var card = scryfall.Cards.GetNamed(null, cardName);
			return $"{card.Name}";
		}

		public static double? getUsd(string cardName)
		{
			var scryfall = new ScryfallClient();

			var card = scryfall.Cards.GetNamed(null, cardName);
			return card.Usd;
		}
	}
}
