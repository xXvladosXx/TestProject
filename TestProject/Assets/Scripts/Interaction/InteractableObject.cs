using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Interaction.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    [RequireComponent(typeof(Animator), 
        typeof(Collider))]
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        [TextArea]
        [SerializeField] private string _textOfInteraction;

        [SerializeField] private Material _highlightMaterial;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private AudioClip _effect;
        [SerializeField] private bool _shouldChangeMaterialInChildren;
        
        private Animator _animator;
        private Renderer _selectionRenderer;
        
        private List<Renderer> _selectionRenderers = new List<Renderer>();

        protected Collider Collider;
        
        private static readonly int Interacted = Animator.StringToHash("Interacted");


        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _selectionRenderer = GetComponent<Renderer>();
            _selectionRenderers = GetComponentsInChildren<Renderer>().ToList();
            
            Collider = GetComponent<Collider>();
        }

        public virtual void Interact(IInteractor interactor)
        {
            AudioManager.Instance.PlayEffectSound(_effect);

            _animator.SetBool(Interacted, true);
            Collider.enabled = false;
        }

        public string InteractionText()
        {
            return _textOfInteraction;
        }

        public void HighlightObject()
        {
            if(_selectionRenderer != null)
                _selectionRenderer.material = _highlightMaterial;

            if(!_shouldChangeMaterialInChildren) return;
            
            foreach (var selectionRenderer in _selectionRenderers)
            {
                selectionRenderer.material = _highlightMaterial;
            }
        }

        public virtual void OnMouseClicked(IInteractor interactor)
        {
            
        }


        public void OnMouseExit()
        {
            if(_selectionRenderer != null)
                _selectionRenderer.material = _defaultMaterial;
            
            if(!_shouldChangeMaterialInChildren) return;
            
            foreach (var selectionRenderer in _selectionRenderers)
            {
                selectionRenderer.material = _defaultMaterial;
            }
        }
    }
}