using System;
using Inventory;
using UnityEngine;

namespace Interaction.Core
{
    public interface IInteractable
    {
        public void Interact(IInteractor interactor);
        public string InteractionText();
        void HighlightObject();
        void OnMouseClicked(IInteractor interactor);
        void OnMouseExit();
    }
}