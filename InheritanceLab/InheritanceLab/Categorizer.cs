/*
* Name: [YOUR NAME HERE]
* South Hills Username: [YOUR SOUTH HILLS USERNAME HERE]
*/

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

        public static void Creator(ProductTypes productType, String brand, String title, int quantityOnHand, float price, List<String> miscAttributes)
        {
            //TODO: Create new objects based on the productType and store them in the InventoryItems list
        }

        public static List<InventoryItem> PerformBubbleSort()
        {
            //TODO: Bubble sort the InventoryList by Price. Return the sorted list when done.
            return null!;
        }
    }
}
