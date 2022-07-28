using System;
using Camera;
using Entities.Core;
using Interaction.Core;
using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Raycast;
using PlayerInput = Utilities.Input.PlayerInput;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput),
        typeof(CameraController))]
    public class Player : AliveEntity, IInteractor
    {
        [field: SerializeField] public ItemPicker ItemPicker { get; private set; }
        [SerializeField] private Vector3 _startPosition;
        
        public PlayerInputActions PlayerInputActions { get; private set; }
        public CameraController CameraController { get; private set; }

        private Vector3 _startRotation;
        private Vector3 _currentPosition;
        private PlayerInput _playerInput;

        public Vector3 Position => transform.position;

        public event Action<IInteractable, IInteractor> OnInteracted;

        
        protected override void Awake()
        {
            base.Awake();

            _playerInput = GetComponent<PlayerInput>();
            CameraController = GetComponent<CameraController>();
            
            _playerInput.Init();
            CameraController.Init(_playerInput);
            
            PlayerInputActions = _playerInput.PlayerInputActions;

            _startRotation = transform.eulerAngles;
            
            DeactivateInputs();
        }

        public void Init()
        {
            ActivateInputs();

            transform.localPosition = _startPosition;
            transform.rotation = Quaternion.Euler(_startRotation);
        }
        
        public void DeactivateInputs()
        {
            enabled = false;
            CameraController.enabled = false;
        }

        public void ActivateInputs()
        {
            enabled = true;
            CameraController.enabled = true;
        }
        
        protected override void OnEnable()
        {
            AddInputCallbacks();
        }

        private void Update()
        {
            var hit = Raycaster.MouseRaycast(CameraController.MainCamera);
            if (!hit.HasValue) return;
            if (hit.Value.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.HighlightObject();
            }
        }

        protected override void OnDisable()
        {
            RemoveInputCallbacks();
        }
        
        private void AddInputCallbacks()
        {
            PlayerInputActions.Player.Click.canceled += OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext obj)
        {
            var hit = Raycaster.MouseRaycast(CameraController.MainCamera);
            if (!hit.HasValue) return;
            if (hit.Value.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.HighlightObject();
                interactable.OnMouseExit();
                OnInteracted?.Invoke(interactable, this);
            }
        }

        private void RemoveInputCallbacks()
        {
            PlayerInputActions.Player.Click.canceled -= OnClickPerformed;
        }

    }
}