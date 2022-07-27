using UnityEngine;

namespace Utilities.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputActions PlayerInputActions { get; private set; }
        private PlayerInputActions.CameraActions CameraActions { get; set; }
        private PlayerInputActions.PlayerActions PlayerActions { get; set; }

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