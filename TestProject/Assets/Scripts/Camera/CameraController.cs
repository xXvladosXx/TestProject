using System;
using Data;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.Input.PlayerInput;

namespace Camera
{
    [RequireComponent(typeof(PlayerInput),
        typeof(Player))]
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField] private CameraControllerData CameraControllerData { get; set; }

        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _centerTransform;

        public UnityEngine.Camera MainCamera { get; private set; }

        private Vector3 _targetPosition;

        private float _currentSpeed;
        private PlayerInputActions _cameraActions;
        private InputAction _movement;

        public void Init(PlayerInput playerInput)
        {
            _cameraActions = playerInput.PlayerInputActions;
            _movement = _cameraActions.Camera.Movement;
        }

        private void Start()
        {
            MainCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            GetMovement();

            if (_cameraActions.Camera.RotationLeft.IsPressed())
            {
                RotateCamera(false);
            }

            if (_cameraActions.Camera.RotationRight.IsPressed())
            {
                RotateCamera(true);
            }

            UpdatePosition();
        }


        private void GetMovement()
        {
            Vector3 inputValue = _movement.ReadValue<Vector2>().x * GetCameraRight() +
                                 _movement.ReadValue<Vector2>().y * GetCameraForward();

            if (inputValue.sqrMagnitude > .1f)
                _targetPosition += inputValue;
        }

        private Vector3 GetCameraForward()
        {
            var forward = _cameraTransform.forward;
            forward.y = 0;

            return forward;
        }

        private Vector3 GetCameraRight()
        {
            var right = _cameraTransform.right;
            right.y = 0;

            return right;
        }

        private void UpdatePosition()
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, CameraControllerData.MaxSpeed,
                Time.deltaTime * CameraControllerData.Acceleration);

            transform.position += _targetPosition * (_currentSpeed * Time.deltaTime);

            _targetPosition = Vector3.zero;
        }

        private void RotateCamera(bool right)
        {
            if (right)
            {
                transform.RotateAround(transform.position, _centerTransform.up,
                    CameraControllerData.MaxRotationSpeed * Time.deltaTime);
                return;
            }

            transform.RotateAround(transform.position, _centerTransform.up,
                -CameraControllerData.MaxRotationSpeed * Time.deltaTime);
        }
    }
}