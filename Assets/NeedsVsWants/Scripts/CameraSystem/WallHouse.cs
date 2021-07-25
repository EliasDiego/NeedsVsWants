using System.Collections;

using System.Collections.Generic;

using UnityEngine;


namespace NeedsVsWants.CameraSystem
{
    public class WallHouse : MonoBehaviour
    {



        public MeshRenderer Roof = null;
        public GameObject Phaser = null;
        

        void OnTriggerEnter(Collider collider)

        {

            Roof.enabled = false;
            
            

            

        }

        void OnTriggerExit(Collider collider)

        {
            Roof.enabled = true;

            // collider.gameObject.SetActive(true);

        }

        private bool IsCharacter(Collider collider)

        {
            GameObject Phaser = collider.gameObject.GetComponent<GameObject>();
            
            return gameObject == collider.gameObject;  

        }



     



         

    }
}