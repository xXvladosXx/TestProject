using Interaction;
using Interaction.Core;
using Inventory.Core;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemInteractableObject : InteractableObject
    {
        [field: SerializeField] public Item Item { get; private set; }
        public override bool TryToInteract(IInteractor interactor)
        {
            base.TryToInteract(interactor);

            interactor.ItemPicker.Inventory.ItemContainer.TryToAddItem(new ItemSlot(Item));
            
            Destroy(gameObject);
            
            return true;
        }
    }
}