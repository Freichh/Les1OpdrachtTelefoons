using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les1OpdrachtTelefoons.Entities
{
    public class Phone
    {
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public Phone(string brand, string type, int price, string description)
        {
            this.Brand = brand;
            this.Type = type;
            this.Price = price;
            this.Description = description;
        }
    }
}
