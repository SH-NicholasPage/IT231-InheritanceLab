using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceLab
{
    public class Categorizer
    {
        public static List<InventoryItem> InventoryItems { get; private set; } = new List<InventoryItem>();

        public static void Categorize(ProductTypes productType, String brand, String title, int quantityOnHand, float price, List<String> miscAttributes)
        {
            
        }

        public static List<InventoryItem> PerformBubbleSort()
        {

            return InventoryItems;
        }
    }
}
