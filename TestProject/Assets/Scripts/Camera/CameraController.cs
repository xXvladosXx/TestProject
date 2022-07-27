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
        private Vector3 _velocity;
        
        private float _zoomHeight;
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

            UpdateCameraPosition();
            UpdatePosition();
        }

    
        private void GetMovement()
        {
            Vector3 inputValue = _movement.ReadValue<Vector2>().x * GetCameraRight() +
                                 _movement.ReadValue<Vector2>().y * GetCameraForward();
            
            if(inputValue.sqrMagnitude > .1f)
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
            if (_targetPosition.sqrMagnitude > .1f)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, CameraControllerData.MaxSpeed,
                    Time.deltaTime * CameraControllerData.Acceleration);

                transform.position += _targetPosition * (_currentSpeed * Time.deltaTime);
            }
            else
            {
                if (!enabled)
                {
                    _velocity = Vector3.zero;
                }
                else
                {
                    _velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime * CameraControllerData.Damping);
                    transform.position += _velocity * Time.deltaTime;
                }
            }
            
            _targetPosition = Vector3.zero;
        }

        private void UpdateCameraPosition()
        {
            var localPosition = _cameraTransform.localPosition;
            
            Vector3 zoomTarget = new Vector3(localPosition.x, _zoomHeight, localPosition.z);
            zoomTarget -= CameraControllerData.ZoomSpeed * (_zoomHeight - localPosition.y) * Vector3.forward;
            
            _cameraTransform.localPosition = Vector3.Lerp(localPosition, zoomTarget, Time.deltaTime * CameraControllerData.ZoomDamping);
            _cameraTransform.LookAt(transform);
        }
        
        private void RotateCamera(bool right)
        {
            if (right)
            {
                transform.RotateAround(transform.position, _centerTransform.up, CameraControllerData.MaxRotationSpeed * Time.deltaTime);
                return;
            }
            
            transform.RotateAround(transform.position, _centerTransform.up, -CameraControllerData.MaxRotationSpeed * Time.deltaTime);
        }

    }
}
