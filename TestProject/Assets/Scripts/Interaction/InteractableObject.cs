using System;
using Interaction.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private string TextOfInteraction;
        
        public event Action<GameObject> OnInteracted;
        
        public virtual bool TryToInteract(IInteractor interactor)
        {
            print("With " + gameObject);
            return true;
        }

        public string InteractionText(IInteractor interactor)
        {
            return TextOfInteraction;
        }

        public virtual void OnMouseEnter()
        {
            print("Color changed");
        }
    }
}