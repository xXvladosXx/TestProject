using System;
using Interaction.Core;
using Inventory.Core;
using TransformChange;
using UnityEngine;
using Utilities.Extensions;

namespace Interaction
{
    public class ItemInteractableObject : InteractableObject, IHashable
    {
        [field: SerializeField] private Item Item { get; set; }
        [field: SerializeField] private float _timeToDestroyItem = .5f;
        [field: SerializeField] private float _speedModifier = 4f;
        [field: SerializeField] private ParticleSystem _particle;

        private Rotation _rotation;
        private Vector3 _pickerPosition;
        private bool _itemPicked;
        protected override void Awake()
        {
            base.Awake();
            _rotation = GetComponent<Rotation>();
        }

        private void Update()
        {
            if (_itemPicked)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                
                transform.Translate(transform.up * (_speedModifier * Time.deltaTime));
            }
        }

        public override void Interact(IInteractor interactor)
        {
            base.Interact(interactor);
            
            Destroy(_rotation);
            
            Instantiate(_particle, transform.position, Quaternion.identity, transform);
            
            _itemPicked = true;
            _pickerPosition = interactor.Position;
            
            interactor.ItemPicker.Inventory.ItemContainer.TryToAddItem(new ItemSlot(Item));
            
            this.CallWithDelay(DeactivateKey, _timeToDestroyItem);
        }

        private void DeactivateKey()
        {
            gameObject.SetActive(false);
        }


        public string Hash
        {
            get => Item.Hash;
            set => Item.Hash = value;
        }
    }
}