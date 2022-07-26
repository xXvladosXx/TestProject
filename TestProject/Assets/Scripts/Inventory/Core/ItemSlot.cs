using System;
using System.Linq;
using UnityEngine;

namespace Inventory.Core
{
    [Serializable]
    public class ItemSlot
    {
        public Item Item;
        public int ID;

        public ItemType[] ItemTypes;
        
        public ItemSlot()
        {
            Item = null;
            ID = -1;
            ItemTypes = new ItemType[]{};
        }
        public ItemSlot(Item item)
        {
            Item = item;
            ID = item.ItemData.ID;
        } 
       
        public void RemoveItem()
        {
            Item = null;
            ID = -1;
        }

        public void UpdateSlot(ItemSlot item)
        {
            Item = item.Item;
            ID = item.ID;
        }
    }
}