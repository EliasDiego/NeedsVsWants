using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeedsVsWants.CameraSystem
{



    public class CameraControls : MonoBehaviour
    {
        [SerializeField] InputActionReference camerabindings;
        [SerializeField] float rotateSpeed = 1f;
        private bool isPerformed = false;
        private float performValue;
        
        void Start()
        {
            camerabindings.action.actionMap.Enable();
            camerabindings.action.performed += StartCameraMovement;
           // camerabindings.CameraZoom.performed += Zoom();




        }

        void StartCameraMovement (InputAction.CallbackContext context)
        {
            
            Vector3 rotation = transform.eulerAngles;

            //Debug.Log(context.ReadValue <float>());

            
            rotation.y += rotateSpeed* Time.deltaTime;

            transform.eulerAngles = rotation;

            

          
        }

        void Zoom ()
        {
            Debug.Log("Zoom!");
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