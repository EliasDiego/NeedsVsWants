using System.Collections;

using System.Collections.Generic;

using UnityEngine;


namespace NeedsVsWants.CameraSystem
{
    public class WallHouse : MonoBehaviour
    {



        public GameObject Roof = null;
        public GameObject Phaser = null;


        void OnTriggerEnter(Collider collider)

        {



            collider.gameObject.SetActive(false);

                //Debug.Log("Hits!");
               // SetMaterialTransparent();

                //iTween.FadeTo(Roof, 0, 1);

            

        }

        void OnTriggerExit(Collider collider)

        {

            collider.gameObject.SetActive(true);

        }

        private bool IsCharacter(Collider collider)

        {
            GameObject Phaser = collider.gameObject.GetComponent<GameObject>();
            
            return gameObject == collider.gameObject;  

        }



     



         

    }
}