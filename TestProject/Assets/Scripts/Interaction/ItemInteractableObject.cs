using Interaction.Core;
using Inventory.Core;
using UnityEngine;

namespace Interaction
{
    public class ItemInteractableObject : InteractableObject
    {
        [field: SerializeField] private Item Item { get; set; }
        [field: SerializeField] private float _timeToDestroyItem = .5f;
        
        public override void Interact(IInteractor interactor)
        {
            base.Interact(interactor);

            interactor.ItemPicker.Inventory.ItemContainer.TryToAddItem(new ItemSlot(Item));
            
            DestroyObject();
        }

        private void DestroyObject()
        {
            Destroy(gameObject, _timeToDestroyItem);
        }
    }
}