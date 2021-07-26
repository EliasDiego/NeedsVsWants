using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.MenuSystem
{
    public class BoxPopUp : PopUp
    {
        [Header("Box")]
        [SerializeField]
        Image _BoxImage;
        [SerializeField]
        float _ScaleSpeed;

        Coroutine _BoxScaleAnimation;
        
        IEnumerator AnimateScale(RectTransform rectTransform, Vector3 targetScale, float delta, System.Action onAfterScale)
        {
            while(rectTransform.localScale != targetScale)
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, delta * (isDeltaTimeScaled ? Time.deltaTime : Time.unscaledDeltaTime));

                yield return new WaitForEndOfFrame();
            }

            onAfterScale?.Invoke();
        }

        protected virtual void OnEnableBoxPopUp() { }
        protected virtual void OnDisableBoxPopUp() { }
        
        protected override void OnEnablePopUp()
        {
            if(_BoxScaleAnimation != null)
                StopCoroutine(_BoxScaleAnimation);
            
            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.one, _ScaleSpeed, null));
        }

        protected override void OnDisablePopUp()
        {
            if(_BoxScaleAnimation != null)
                StopCoroutine(_BoxScaleAnimation);
                
            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.zero, _ScaleSpeed, () => transform.SetActiveChildren(false)));
        }
    }
}