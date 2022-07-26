using System;
using Data;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    [RequireComponent(typeof(PlayerInput),
        typeof(Player))]
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField] public CameraControllerData CameraControllerData { get; private set; }
        
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _centerTransform;

        public UnityEngine.Camera MainCamera { get; private set; }
        
        private Vector3 _targetPosition;
        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private Vector3 _onDragStarted;
        
        private float _zoomHeight;
        private float _currentSpeed;
        private PlayerInputActions.CameraActions _cameraActions;
        private InputAction _movement;

        public void Init(PlayerInput playerInput)
        {
            _cameraActions = playerInput.PlayerInputActions.Camera;
        }

        private void Start()
        {
            MainCamera = UnityEngine.Camera.main;
        }

        private void OnEnable()
        {
            _lastPosition = _targetPosition;
            _movement = _cameraActions.Movement;

            AddInputCallbacks();
        }

        private void AddInputCallbacks()
        {
            _cameraActions.Zoom.performed += ZoomCamera;
        }

       
        private void Update()
        {
            GetMovement();
            
            if (_cameraActions.RotationLeft.IsPressed())
            {
                RotateCamera(false);
            }
            if (_cameraActions.RotationRight.IsPressed())
            {
                RotateCamera(true);
            }

            UpdateVelocity();
            UpdateCameraPosition();
            UpdatePosition();
        }

        private void OnDisable()
        {
            RemoveInputCallbacks();
        }

        private void RemoveInputCallbacks()
        {
            _cameraActions.Zoom.performed -= ZoomCamera;
        }

        private void UpdateVelocity()
        {
            var position = transform.position;
            
            _velocity = (position - _lastPosition) / Time.deltaTime;
            _velocity.y = 0;
            _lastPosition = position;
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
                _velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime * CameraControllerData.Damping);
                transform.position += _velocity * Time.deltaTime;
            }
            
            _targetPosition = Vector3.zero;
        }
        
        private void ZoomCamera(InputAction.CallbackContext input)
        {
            var value = -input.ReadValue<Vector2>().y / 100f;

            if (!(Mathf.Abs(value) > .1f)) return;
            
            _zoomHeight = _cameraTransform.localPosition.y + value * CameraControllerData.SizePerScroll;

            if (_zoomHeight < CameraControllerData.MinHeight)
            {
                _zoomHeight = CameraControllerData.MinHeight;
            }else if (_zoomHeight > CameraControllerData.MaxHeight)
            {
                _zoomHeight = CameraControllerData.MaxHeight;
            }
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
