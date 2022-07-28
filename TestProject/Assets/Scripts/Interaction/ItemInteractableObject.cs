﻿using System;
using Interaction.Core;
using Inventory.Core;
using TransformChange;
using UnityEngine;

namespace Interaction
{
    public class ItemInteractableObject : InteractableObject
    {
        [field: SerializeField] private Item Item { get; set; }
        [field: SerializeField] private float _timeToDestroyItem = .5f;
        [field: SerializeField] private float _speedModifier = 4f;

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
                var angle = (_pickerPosition - transform.position).normalized;
                transform.forward = angle;
                transform.Translate(transform.forward * (_speedModifier * Time.deltaTime));
            }
        }

        public override void Interact(IInteractor interactor)
        {
            base.Interact(interactor);
            Destroy(_rotation);
            
            _itemPicked = true;
            _pickerPosition = interactor.Position;
            
            interactor.ItemPicker.Inventory.ItemContainer.TryToAddItem(new ItemSlot(Item));
            
            DestroyObject();
        }

        private void DestroyObject()
        {
            Destroy(gameObject, _timeToDestroyItem);
        }
    }
}