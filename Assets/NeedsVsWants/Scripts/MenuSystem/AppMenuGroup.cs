using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.MenuSystem
{
    [RequireComponent(typeof(Image), typeof(Mask))]
    public class AppMenuGroup : MenuGroup
    {
        [SerializeField]
        float _AppAnimationSpeed = 1f;

        RectTransform _RectTransform;

        Coroutine _AppAnimationCoroutine;

        void Awake() 
        {
            _RectTransform = (RectTransform)transform;

            _RectTransform.sizeDelta = Vector2.zero;
        }

        protected override void OnDisableGroup()
        {
            if(_AppAnimationCoroutine != null)
                StopCoroutine(_AppAnimationCoroutine);

            _AppAnimationCoroutine = StartCoroutine(SetSizeDelta(Vector2.zero, () => currentMenu?.DisableMenu()));
        }

        protected override void OnEnableGroup()
        {
            if(_AppAnimationCoroutine != null)
                StopCoroutine(_AppAnimationCoroutine);

            currentMenu?.EnableMenu();

            _AppAnimationCoroutine = StartCoroutine(SetSizeDelta((transform.parent as RectTransform).sizeDelta, null));
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