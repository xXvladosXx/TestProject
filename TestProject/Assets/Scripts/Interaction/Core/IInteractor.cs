using System;
using Inventory;

namespace Interaction.Core
{
    public interface IInteractor
    {
        public event Action<IInteractable, IInteractor> OnInteracted;
        ItemPicker ItemPicker { get; }
    }
}