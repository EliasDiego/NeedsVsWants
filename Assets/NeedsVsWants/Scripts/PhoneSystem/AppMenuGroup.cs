using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.PhoneSystem
{
    public class AppMenuGroup : MenuGroup
    {
        [SerializeField]
        float _AppAnimationSpeed = 1f;

        RectTransform _RectTransform;

        Coroutine _AppAnimationCoroutine;

        void Awake() 
        {
            _RectTransform = (RectTransform)transform;

            //_RectTransform.sizeDelta = Vector2.zero;
            _RectTransform.localScale = Vector3.zero;
        }

        protected override void OnDisableGroup()
        {
            if(_AppAnimationCoroutine != null)
                StopCoroutine(_AppAnimationCoroutine);

            //_AppAnimationCoroutine = StartCoroutine(SetSizeDelta(Vector2.zero, () => currentMenu?.DisableMenu()));
            _AppAnimationCoroutine = StartCoroutine(SetScale(Vector3.zero, () => currentMenu?.DisableMenu()));
        }

        protected override void OnEnableGroup()
        {
            if(_AppAnimationCoroutine != null)
                StopCoroutine(_AppAnimationCoroutine);

            currentMenu?.EnableMenu();

            //_AppAnimationCoroutine = StartCoroutine(SetSizeDelta((transform.parent as RectTransform).sizeDelta, null));
            _AppAnimationCoroutine = StartCoroutine(SetScale(Vector3.one, null));
        }

        IEnumerator SetScale(Vector3 targetScale, System.Action onAfterScale)
        {
            while(_RectTransform.localScale != targetScale)
            {
                _RectTransform.localScale = Vector3.Lerp(_RectTransform.localScale, targetScale, _AppAnimationSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            
            onAfterScale?.Invoke();
        }

        IEnumerator SetSizeDelta(Vector2 targetSizeDelta, System.Action onAfterSize)
        {
            while(_RectTransform.sizeDelta != targetSizeDelta)
            {
                _RectTransform.sizeDelta = Vector2.Lerp(_RectTransform.sizeDelta, targetSizeDelta, _AppAnimationSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            onAfterSize?.Invoke();
        }
    }
}