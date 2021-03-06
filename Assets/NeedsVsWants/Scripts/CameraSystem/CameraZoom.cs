using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeedsVsWants.CameraSystem
{



    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] InputActionReference camerazoom;
        [SerializeField] float zoomspeed = 3f;
       [SerializeField] float minFOV = 5f;
        [SerializeField] float maxFOV = 50f;
        private Camera cameraFOV;

        void Start()
        {
            camerazoom.action.actionMap.Enable();
            camerazoom.action.performed += StartCameraZoom;
            cameraFOV = GetComponent<Camera>();
      




        }

        void StartCameraZoom(InputAction.CallbackContext context)
        {
            
            float zoominput = context.ReadValue<float>();
            cameraFOV.orthographicSize = Mathf.Clamp(cameraFOV.orthographicSize + zoominput * zoomspeed * Time.deltaTime, minFOV, maxFOV) ;
            
        }

    

    }
}