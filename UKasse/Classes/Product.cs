using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UKasse.Classes
{
    internal class Product:IComparable<Product>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
        public bool isOffen { get; set; }
        public int PTypeId { get; set; }
        public string? Barcode { get; set; }
        public Product() 
        {
            isOffen = false;
        }
        public int CompareTo(Product? other)
        {
            // Compare based on the last name
            return string.Compare(this.Name, other?.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
