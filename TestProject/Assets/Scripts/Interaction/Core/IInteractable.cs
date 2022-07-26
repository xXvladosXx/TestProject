using System;
using Inventory;
using UnityEngine;

namespace Interaction.Core
{
    public interface IInteractable
    {
        public event Action<GameObject> OnInteracted;
        public bool TryToInteract(IInteractor interactor);
        public string InteractionText(IInteractor interactor);

        public void OnMouseEnter();
    }
}