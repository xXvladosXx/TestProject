using Interaction;
using Interaction.Core;

namespace Inventory.Items
{
    public class ChestInteractableObject : InteractableObject
    {
        public override bool TryToInteract(IInteractor interactor)
        {
            base.TryToInteract(interactor);
            Destroy(gameObject);

            return true;
        }
    }
}