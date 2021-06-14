using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeedsVsWants.CameraSystem
{



    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] InputAction camerarotate;
        [SerializeField] float rotatespeed = 15.0f;
        private CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        void OnEnable()
        {
            camerarotate.Enable();
        }

        void OnDisable()
        {
            camerarotate.Disable();
        }
        
        void Update()
        {
            
            Vector2 inputVector = camerarotate.ReadValue<Vector2>();

           //Vector3 finalVector = new Vector3();
           // inputVector.x = finalVector.z;
           // finalVector.x = inputVector.x;
           // finalVector.z = inputVector.y;

            transform.eulerAngles += (Vector3)inputVector * Time.deltaTime * rotatespeed;
        }
    }
}
