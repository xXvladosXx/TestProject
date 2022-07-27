using System;
using Interaction.Core;
using Inventory.Core;
using UnityEngine;

namespace Interaction.Requirements
{
    [Serializable]
    public class ItemRequirement : Requirement
    {
        [field: SerializeField] public Item[] Items { get; private set; }
        
        public override bool AllRequirementsChecked(IInteractor interactor)
        {
            foreach (var item in Items)
            {
                return interactor.ItemPicker.Inventory.ItemContainer.TryToGetItem(item);
            }

            return true;
        }
    }
}