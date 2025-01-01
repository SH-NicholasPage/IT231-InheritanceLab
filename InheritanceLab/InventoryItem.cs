using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceLab
{
    public class InventoryItem
    {
        //TODO: The other common properties go here!
        public float Price { get; }//Can only be assigned to in a constructor in this class

        public virtual void DisplayItem()
        {
            //TODO: Implement the DisplayItem method
            throw new NotImplementedException();
        }
    }
}
