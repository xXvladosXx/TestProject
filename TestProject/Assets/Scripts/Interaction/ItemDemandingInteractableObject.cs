using Interaction;
using Interaction.Core;
using Inventory.Core;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemDemandingInteractableObject : InteractableObject
    {
        [field: SerializeField] public Item Item { get; private set; }
        public override bool TryToInteract(IInteractor interactor)
        {
            base.TryToInteract(interactor);

            if (interactor.ItemPicker.Inventory.ItemContainer.TryToGetItem(Item))
            {
                return true;
            }

            print("Dont have permission");
            return false;
        }
    }
}