using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeedsVsWants.CameraSystem
{



    public class CameraControls : MonoBehaviour
    {
        [SerializeField] InputActionReference camerabindings;
        [SerializeField] float panSpeed = 20f;

        void Start()
        {
            camerabindings.action.actionMap.Enable();
            camerabindings.action.performed += StartCameraMovement; 
        }

        void StartCameraMovement (InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue <float>());

        }
        // Update is called once per frame
        void Update()
        {
           /* Vector3 pos = transform.position;

            if (Input.GetKey("w"))
            {
                pos.z += panSpeed * Time.deltaTime;
            }

            if (Input.GetKey("s"))
            {
                pos.z -= panSpeed * Time.deltaTime;
            }

            if (Input.GetKey("d"))
            {
                pos.z += panSpeed * Time.deltaTime;
            }

            if (Input.GetKey("a"))
            {
                pos.z -= panSpeed * Time.deltaTime;
            }

            transform.position = pos;*/
        }
    }
}