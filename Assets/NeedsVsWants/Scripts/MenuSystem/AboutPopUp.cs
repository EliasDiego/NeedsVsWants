using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.MenuSystem
{
    public class AboutPopUp : PopUp
    {
        [SerializeField]
        Image _BoxImage;
        [SerializeField]
        float _ScaleSpeed;

        Coroutine _BoxScaleAnimation;

        IEnumerator AnimateScale(RectTransform rectTransform, Vector3 targetScale, float delta, System.Action onAfterScale)
        {
            while(rectTransform.localScale != targetScale)
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, delta * Time.unscaledDeltaTime);

                yield return new WaitForEndOfFrame();
            }

            onAfterScale?.Invoke();
        }
        protected override void OnEnablePopUp()
        {
            if(_BoxScaleAnimation != null)
                StopCoroutine(_BoxScaleAnimation);

            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.one, _ScaleSpeed, null));

            transform.SetActiveChildren(true);
        }

        protected override void OnDisablePopUp()
        {
            if(_BoxScaleAnimation != null)
                StopCoroutine(_BoxScaleAnimation);
                
            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.zero, _ScaleSpeed, () => transform.SetActiveChildren(false)));
        }
    }
}