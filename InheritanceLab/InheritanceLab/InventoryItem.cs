using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceLab
{
    public class InventoryItem
    {
        public float Price { get; set; }

        public virtual void DisplayItem()
        {
            Console.WriteLine("[OVERRIDE ME IN ALL DERIVED CLASSES]");
        }
    }
}
