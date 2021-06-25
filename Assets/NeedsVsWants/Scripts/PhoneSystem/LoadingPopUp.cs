using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.PhoneSystem
{
    public class LoadingPopUp : PopUp
    {
        [SerializeField]
        Image _LoadingScreen;

        Coroutine _AnimateRotationCoroutine;
        
        IEnumerator AnimateRotation(RectTransform rectTransform, Quaternion deltaRotation, float time, System.Action onAfterRotation)
        {
            while(time > 0)
            {
                rectTransform.localRotation *= deltaRotation;

                time -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            onAfterRotation?.Invoke();
        }

        protected override void onEnablePopUp()
        {
            if(_AnimateRotationCoroutine != null)
                StopCoroutine(_AnimateRotationCoroutine);

            _AnimateRotationCoroutine = StartCoroutine(AnimateRotation(_LoadingScreen.rectTransform, Quaternion.Euler(0, 0, 1), 2f, null));
        }

        protected override void onDisablePopUp()
        {
            if(_AnimateRotationCoroutine != null)
                StopCoroutine(_AnimateRotationCoroutine);
                
            _LoadingScreen.gameObject.SetActive(false);
        }
    }
}