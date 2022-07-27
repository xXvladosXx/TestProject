using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities.Raycast
{
    public sealed class Raycaster
    {
        public static RaycastHit? MouseRaycast(UnityEngine.Camera from, LayerMask layerMask, Vector3 offset = default)
        {
            var ray = from.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            ray.origin += offset;
            
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity,
                    layerMask))
            {
                return raycastHit;
            }

            return null;
        }
        
        public static RaycastHit? MouseRaycast(UnityEngine.Camera from, Vector3 offset = default)
        {
            var ray = from.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            ray.origin += offset;
            
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                return raycastHit;
            }

            return null;
        }
    }
}