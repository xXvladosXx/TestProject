using System;
using Interaction.Core;
using UnityEngine;

namespace Interaction.Requirements
{
    [Serializable]
    public class HashRequirement : Requirement
    {
        [field: SerializeField] public string Hash { get; set; }

        public override bool AllRequirementsChecked(IInteractor interactor)
        {
            foreach (var itemSlot in interactor.ItemPicker.Inventory.ItemContainer.ItemSlots)
            {
                if (itemSlot.Item == null) continue;
                if (itemSlot.Item.Hash == Hash)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}