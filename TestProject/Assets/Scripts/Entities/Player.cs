using System;
using Camera;
using Entities.Core;
using Interaction.Core;
using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Raycast;
using PlayerInput = Camera.PlayerInput;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput),
        typeof(CameraController))]
    public class Player : AliveEntity, IInteractor
    {
        [field: SerializeField] public ItemPicker ItemPicker { get; private set; }

        public PlayerInput PlayerInput;
        public PlayerInputActions PlayerInputActions { get; private set; }
        public CameraController CameraController { get; private set; }

        public event Action<IInteractable> OnTryToInteract;
        
        protected override void Awake()
        {
            base.Awake();

            PlayerInput = GetComponent<PlayerInput>();
            CameraController = GetComponent<CameraController>();
            
            PlayerInput.Init();
            CameraController.Init(PlayerInput);
            
            PlayerInputActions = PlayerInput.PlayerInputActions;
        }

        protected override void OnEnable()
        {
            AddInputCallbacks();
        }

        protected override void OnDisable()
        {
            RemoveInputCallbacks();
        }
        
        private void AddInputCallbacks()
        {
            PlayerInputActions.Player.Click.performed += OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext obj)
        {
            var hit = Raycaster.GetRaycastOfMouse(CameraController.MainCamera);

            if (!hit.HasValue) return;
            print("Clicked");
            if (hit.Value.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.TryToInteract(this);
                OnTryToInteract?.Invoke(interactable);
            }
        }

        private void RemoveInputCallbacks()
        {
            PlayerInputActions.Player.Click.performed -= OnClickPerformed;
        }

    }
}