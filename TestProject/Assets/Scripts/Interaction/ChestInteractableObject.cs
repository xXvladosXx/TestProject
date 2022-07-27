using Interaction.Core;

namespace Interaction
{
    public class ChestInteractableObject : InteractableObject
    {
        public override void Interact(IInteractor interactor)
        {
            base.Interact(interactor);
            Destroy(this);
        }
    }
}