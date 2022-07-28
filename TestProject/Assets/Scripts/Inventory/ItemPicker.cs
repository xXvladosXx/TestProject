using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class ItemPicker
    {
        [field: SerializeField] public Core.Inventory Inventory { get; private set; }

        public void ResetInventory()
        {
            Inventory.ItemContainer.Clear();
        }
    }
}