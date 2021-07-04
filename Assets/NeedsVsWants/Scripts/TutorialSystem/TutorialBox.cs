using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.MenuSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.TutorialSystem
{
    public class TutorialBox : PopUp
    {
        [SerializeField]
        Image _BoxImage;
        [SerializeField]
        float _ScaleSpeed;

        Coroutine _BoxScaleAnimation;

        protected override bool controlSetActive => true;

        async void Start() 
        {
            await System.Threading.Tasks.Task.Delay(500);

            _BoxImage.rectTransform.localScale = Vector3.zero;    

            EnablePopUp();
        }
        
        IEnumerator AnimateScale(RectTransform rectTransform, Vector3 targetScale, float delta, System.Action onAfterScale)
        {
            while(rectTransform.localScale != targetScale)
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, delta * Time.unscaledDeltaTime);

                yield return new WaitForEndOfFrame();
            }

            onAfterScale?.Invoke();
        }

        protected override void onEnablePopUp()
        {
            if(_BoxScaleAnimation != null)
                StopCoroutine(_BoxScaleAnimation);

            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.one, _ScaleSpeed, null));

            transform.SetActiveChildren(true);
        }

        protected override void onDisablePopUp()
        {
            if(_BoxScaleAnimation != null)
                StopCoroutine(_BoxScaleAnimation);
                
            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.zero, _ScaleSpeed, () => transform.SetActiveChildren(false)));
        }
    }
}