using System;
using UnityEngine;

namespace Inventory.Core
{
    [Serializable]
    public class ItemContainer
    {
        [field: SerializeField] public ItemSlot[] ItemSlots { get; private set; }
        [field: SerializeField] public ItemDatabase Database { get; private set; }

        public event Action OnItemUpdate;

        public ItemContainer(int size)
        {
            ItemSlots = new ItemSlot[size];
        }

        public bool TryToAddItem(ItemSlot itemSlot)
        {
            ItemSlots[0] = new ItemSlot(itemSlot.Item);
            
            OnItemUpdate?.Invoke();
            return true;
        }
        
        public bool TryToGetItem(Item item)
        {
            foreach (var itemSlot in ItemSlots)
            {
                if (itemSlot.Item == null) continue;
                
                if (itemSlot.Item == item)
                    return true;
            }

            return false;
        }
    }
}