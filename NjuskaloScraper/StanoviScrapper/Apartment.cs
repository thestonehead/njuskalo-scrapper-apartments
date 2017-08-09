using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanoviScrapper
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public Location Location { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Neighbourhood { get; set; }
        public string Type { get; set; }
        public string Levels { get; set; }
        public string Rooms { get; set; }
        public string Floor { get; set; }
        public bool? Lift { get; set; }
        public float? Area { get; set; }
        public float? TerraceArea { get; set; }
        public int? BuiltYear { get; set; }
        public string AvailableFrom { get; set; }
        public string Utilities { get; set; }
        public string Furniture { get; set; }
        public float? Price { get; set; }
        public string Orientation { get; set; }
        public bool? CloseToTram { get; set; }
        public string Entrance { get; set; }
        public int? ParkingSpaces { get; set; }
        public string EnergyLevel { get; set; }
        public string Description { get; set; }
        public dynamic BalconyArea { get; internal set; }
        public dynamic LastAdaptationYear { get; internal set; }
        public dynamic CloseToBus { get; internal set; }
        public DateTime Published { get; internal set; }
        public string Seller { get; internal set; }
        public string Title { get; internal set; }


        public bool Hide { get; set; }
        public bool Star { get; set; }
        public bool IsNew { get; set; }
    }
}
