using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeedsVsWants.CameraSystem
{



    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] InputActionReference camerazoom;
        [SerializeField] float zoomspeed = 10f;
        [SerializeField] float cameraDistance = 10f;

        void Start()
        {
            camerazoom.action.actionMap.Enable();
            camerazoom.action.performed += StartCameraZoom;
      




        }

        void StartCameraZoom(InputAction.CallbackContext context)
        {
            
            float zoominput = context.ReadValue<float>();
            cameraDistance += zoominput * zoomspeed * Time.deltaTime;
            
        }

    

    }
}