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
        [SerializeField]
        bool _IsDeltaTimeScaled = true;

        bool _IsActive = false;

        Coroutine _PanelColorTransition;

        protected virtual bool controlSetActive => false;
        protected virtual bool hasEnabledColorTransition => true;
        protected virtual bool hasDisabledColorTransition => true;

        protected bool isDeltaTimeScaled => _IsDeltaTimeScaled;

        protected Image panel => _Panel;
    
        public bool isActive => _IsActive;

        protected virtual void Awake() 
        {
            transform.SetActiveChildren(false);
        }

        IEnumerator AnimateColor(Image image, Color color, float delta, System.Action onAfterColor)
        {
            while(image.color != color)
            {
                image.color = Color.Lerp(image.color, color, delta * (_IsDeltaTimeScaled ? Time.deltaTime : Time.unscaledDeltaTime));

                yield return new WaitForEndOfFrame();
            }

            onAfterColor?.Invoke();
        }

        protected virtual void OnEnablePopUp() { }
        protected virtual void OnDisablePopUp() { }

        public void EnablePopUp()
        {
            _Panel.color = _StartColor;
            
            if(_PanelColorTransition != null)
                StopCoroutine(_PanelColorTransition);

            if(hasEnabledColorTransition)
            {
                _Panel.color = _StartColor;

                _PanelColorTransition = StartCoroutine(AnimateColor(_Panel, _EnabledColor, _TransitionSpeed, null));
            }

            if(!controlSetActive)
                transform.SetActiveChildren(true);

            _Panel.raycastTarget = true;

            OnEnablePopUp();
        }

        public void DisablePopUp()
        {
            if(_PanelColorTransition != null)
                StopCoroutine(_PanelColorTransition);
            
            if(hasDisabledColorTransition)
            {
                _Panel.color = _EnabledColor;

                _PanelColorTransition = StartCoroutine(AnimateColor(_Panel, _DisabledColor, _TransitionSpeed, () => 
                {
                    if(!controlSetActive)
                        transform.SetActiveChildren(false);
                }));
            }

            else if(!controlSetActive)
                transform.SetActiveChildren(false);

            _Panel.raycastTarget = false;
            
            OnDisablePopUp();
        }
    }
}