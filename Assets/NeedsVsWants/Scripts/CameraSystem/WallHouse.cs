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





                Debug.Log("Hits!");
               // SetMaterialTransparent();

                //iTween.FadeTo(Roof, 0, 1);

            

        }

        private void Update()
        {
            
        }

        private bool IsCharacter(Collider collider)

        {
            GameObject Phaser = collider.gameObject.GetComponent<GameObject>();
            
            return gameObject == collider.gameObject;  

        }



        private void SetMaterialTransparent()

        {

            foreach (Material m in Roof.GetComponent<Renderer>().materials)

            {

                m.SetFloat("_Mode", 2);

                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                m.SetInt("_ZWrite", 0);

                m.DisableKeyword("_ALPHATEST_ON");

                m.EnableKeyword("_ALPHABLEND_ON");

                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                m.renderQueue = 3000;

            }

            

        }



        private void SetMaterialOpaque()

        {

            foreach (Material m in Roof.GetComponent<Renderer>().materials)

            {

                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                m.SetInt("_ZWrite", 1);

                m.DisableKeyword("_ALPHATEST_ON");

                m.DisableKeyword("_ALPHABLEND_ON");

                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                m.renderQueue = -1;

            }

        }



        void OnTriggerExit(Collider collider)

        {

            if (IsCharacter(collider))

            {

                // Set material to opaque

                iTween.FadeTo(Roof, 1, 1);



                Invoke("SetMaterialOpaque", 1.0f);

            }

        }

    }
}