using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.MenuSystem
{
    public class PopUp : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField]
        Image _Panel;
        [SerializeField]
        Color _EnabledColor;
        [SerializeField]
        float _TransitionSpeed;

        bool _IsActive = false;

        Coroutine _PanelColorTransition;

        protected virtual bool controlSetActive => false;
    
        public bool isActive => _IsActive;

        protected virtual void Awake() 
        {
            _Panel.color = Color.clear;    

            transform.SetActiveChildren(false);
        }

        IEnumerator AnimateTransparency(Image image, Color color, float delta)
        {
            while(image.color != color)
            {
                image.color = Color.Lerp(image.color, color, delta * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        protected virtual void onEnablePopUp() { }
        protected virtual void onDisablePopUp() { }

        public void EnablePopUp()
        {
            _PanelColorTransition = StartCoroutine(AnimateTransparency(_Panel, _EnabledColor, _TransitionSpeed));

            if(!controlSetActive)
                transform.SetActiveChildren(true);

            onEnablePopUp();
        }

        public void DisablePopUp()
        {
            if(_PanelColorTransition != null)
                StopCoroutine(_PanelColorTransition);
            
            _PanelColorTransition = StartCoroutine(AnimateTransparency(_Panel, Color.clear, _TransitionSpeed));
            
            if(!controlSetActive)
                transform.SetActiveChildren(false);

            onDisablePopUp();
        }
    }
}