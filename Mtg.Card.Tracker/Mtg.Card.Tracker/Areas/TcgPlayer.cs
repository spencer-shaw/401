using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtg.Card.Tracker.Areas
{
    public class TcgPlayer
    {
        //TODO Consume Api here -Shaw
    }

	public class ImageUris
		public string large { get; set; }
		public string png { get; set; }
		public string art_crop { get; set; }
		public string border_crop { get; set; }
	}
		public string future { get; set; }
		public string frontier { get; set; }
		public string modern { get; set; }
		public string legacy { get; set; }
		public string pauper { get; set; }
		public string vintage { get; set; }
		public string penny { get; set; }
		public string commander { get; set; }
		public string __invalid_name__1v1 { get; set; }
		public string duel { get; set; }
		public string brawl { get; set; }
		public string tcgplayer_decks { get; set; }
		public string edhrec { get; set; }
		public string mtgtop8 { get; set; }
	}
		public string amazon { get; set; }
		public string ebay { get; set; }
		public string tcgplayer { get; set; }
		public string magiccardmarket { get; set; }
		public string cardhoarder { get; set; }
		public string card_kingdom { get; set; }
		public string mtgo_traders { get; set; }
		public string coolstuffinc { get; set; }

	public class RootObject
	{
		public string @object { get; set; }
		public string id { get; set; }
		public string oracle_id { get; set; }
		public List<int> multiverse_ids { get; set; }
		public int mtgo_id { get; set; }
		public int mtgo_foil_id { get; set; }
		public string name { get; set; }
		public string lang { get; set; }
		public string uri { get; set; }
		public string scryfall_uri { get; set; }
		public string layout { get; set; }
		public bool highres_image { get; set; }
		public ImageUris image_uris { get; set; }
		public string mana_cost { get; set; }
		public double cmc { get; set; }
		public string type_line { get; set; }
		public string oracle_text { get; set; }
		public List<string> colors { get; set; }
		public List<string> color_identity { get; set; }
		public Legalities legalities { get; set; }
		public bool reserved { get; set; }
		public bool foil { get; set; }
		public bool nonfoil { get; set; }
		public bool oversized { get; set; }
		public bool reprint { get; set; }
		public string set { get; set; }
		public string set_name { get; set; }
		public string set_uri { get; set; }
		public string set_search_uri { get; set; }
		public string scryfall_set_uri { get; set; }
		public string rulings_uri { get; set; }
		public string prints_search_uri { get; set; }
		public string collector_number { get; set; }
		public bool digital { get; set; }
		public string rarity { get; set; }
		public string illustration_id { get; set; }
		public string artist { get; set; }
		public string frame { get; set; }
		public bool full_art { get; set; }
		public string border_color { get; set; }
		public bool timeshifted { get; set; }
		public bool colorshifted { get; set; }
		public bool futureshifted { get; set; }
		public int edhrec_rank { get; set; }
		public string usd { get; set; }
		public string tix { get; set; }
		public string eur { get; set; }
		public RelatedUris related_uris { get; set; }
		public PurchaseUris purchase_uris { get; set; }
    }
}
