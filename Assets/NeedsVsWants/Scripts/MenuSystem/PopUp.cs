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
        Color _StartColor;
        [SerializeField]
        Color _EnabledColor;
        [SerializeField]
        Color _DisabledColor;
        [SerializeField]
        float _TransitionSpeed;

        bool _IsActive = false;

        Coroutine _PanelColorTransition;

        protected virtual bool controlSetActive => false;
    
        public bool isActive => _IsActive;

        protected virtual void Awake() 
        {
            transform.SetActiveChildren(false);
        }

        IEnumerator AnimateColor(Image image, Color color, float delta, System.Action onAfterColor)
        {
            while(image.color != color)
            {
                image.color = Color.Lerp(image.color, color, delta * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            onAfterColor?.Invoke();
        }

        protected virtual void onEnablePopUp() { }
        protected virtual void onDisablePopUp() { }

        public void EnablePopUp()
        {
            _Panel.color = _StartColor;
            _PanelColorTransition = StartCoroutine(AnimateColor(_Panel, _EnabledColor, _TransitionSpeed, null));

            if(!controlSetActive)
                transform.SetActiveChildren(true);

            _Panel.raycastTarget = true;

            onEnablePopUp();
        }

        public void DisablePopUp()
        {
            if(_PanelColorTransition != null)
                StopCoroutine(_PanelColorTransition);
            
            _PanelColorTransition = StartCoroutine(AnimateColor(_Panel, _DisabledColor, _TransitionSpeed, () => 
            {
                if(!controlSetActive)
                    transform.SetActiveChildren(false);
            }));

            _Panel.raycastTarget = false;
            
            onDisablePopUp();
        }
    }
}