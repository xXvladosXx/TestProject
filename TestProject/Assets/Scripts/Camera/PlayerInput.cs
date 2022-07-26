using System;
using UnityEngine;

namespace Camera
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputActions PlayerInputActions { get; private set; }
        public PlayerInputActions.CameraActions CameraActions { get; private set; }
        public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

        public void Init()
        {
            PlayerInputActions = new PlayerInputActions();
            
            CameraActions = PlayerInputActions.Camera;
            PlayerActions = PlayerInputActions.Player;
            
            PlayerInputActions.Enable();
        }


        private void OnDisable()
        {
            PlayerInputActions.Disable();
        }
    }
}