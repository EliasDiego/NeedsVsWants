using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.MenuSystem
{
    public class CheckoutPopUp : PopUp
    {
        [Header("Box")]
        [SerializeField]
        Image _BoxImage;
        [SerializeField]
        float _ScaleSpeed;
        [SerializeField]
        TMP_Text _Text;
        [SerializeField]
        Button _CloseButton;

        [Header("Content")]
        [SerializeField]
        Image _ProcessingImage;
        [SerializeField][Multiline]
        string _SufficientFundsText;
        [SerializeField][Multiline]
        string _InsufficientFundsText;
        
        [Header("Navigation Buttons")]
        [SerializeField]
        Button _BackgroundAppsButton;
        [SerializeField]
        Button _HomeButton;
        [SerializeField]
        Button _ReturnButton;

        Coroutine _BoxScaleAnimation;
        Coroutine _ProcessingRotationAnimation;

        protected override bool controlSetActive => true;

        public bool hasSufficientFunds { get; set; } = false;

        public System.Action onAfterProcessing { get; set; }

        void Start() 
        {
            _BoxImage.rectTransform.localScale = Vector3.zero;    
        }

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
        
        IEnumerator AnimateScale(RectTransform rectTransform, Vector3 targetScale, float delta, System.Action onAfterScale)
        {
            while(rectTransform.localScale != targetScale)
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, delta * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            onAfterScale?.Invoke();
        }

        void OnSufficientFunds()
        {
            _ProcessingImage.gameObject.SetActive(true);
            _CloseButton.gameObject.SetActive(false);
            _Text.gameObject.SetActive(false);

            StartCoroutine(AnimateRotation(_ProcessingImage.rectTransform, Quaternion.Euler(0, 0, 1), 2.5f, () =>
            {
                _ProcessingImage.gameObject.SetActive(false);
                _CloseButton.gameObject.SetActive(true);
                _Text.gameObject.SetActive(true);

                _Text.text = _SufficientFundsText;
            }));
        }

        void OnInsufficientFunds()
        {
            _ProcessingImage.gameObject.SetActive(false);

            _Text.text = _InsufficientFundsText;
        }

        protected override void onEnablePopUp()
        {
            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.one, _ScaleSpeed, null));
            
            transform.SetActiveChildren(true);

            if(hasSufficientFunds)
                OnSufficientFunds();

            else
                OnInsufficientFunds();

            _BackgroundAppsButton.interactable = false;
            _HomeButton.interactable = false;
            _ReturnButton.interactable = false;
        }

        protected override void onDisablePopUp()
        {
            if(hasSufficientFunds)
            {
                onAfterProcessing?.Invoke();

                transform.SetActiveChildren(false);
            }

            else
            {
                if(_BoxScaleAnimation != null)
                    StopCoroutine(_BoxScaleAnimation);
                    
                _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.zero, _ScaleSpeed, () => transform.SetActiveChildren(false)));
            }

            _BackgroundAppsButton.interactable = true;
            _HomeButton.interactable = true;
            _ReturnButton.interactable = true;
        }
    }
}