using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.PhoneSystem
{
    public class CheckoutPopUp : AppPopUp
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

        [Space]
        [SerializeField]
        PopUp _PanelPopUp;

        Coroutine _BoxScaleAnimation;
        Coroutine _ProcessingRotationAnimation;

        bool hasColorTransition = true;

        protected override bool controlSetActive => true;
        protected override bool hasDisabledColorTransition => hasColorTransition;

        public bool hasSufficientFunds { get; set; } = false;
        public bool useDefaultText { get; set; } = true;

        public string customSufficientFundsText { get; set; }
        public string customInsufficientFundsText { get; set; }

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

            hasColorTransition = false;

            StartCoroutine(AnimateRotation(_ProcessingImage.rectTransform, Quaternion.Euler(0, 0, 1), 2.5f, () =>
            {
                _ProcessingImage.gameObject.SetActive(false);
                _CloseButton.gameObject.SetActive(true);
                _Text.gameObject.SetActive(true);

                _Text.text = useDefaultText ? _SufficientFundsText : customSufficientFundsText;
            }));
        }

        void OnInsufficientFunds()
        {
            _ProcessingImage.gameObject.SetActive(false);

            _Text.text = useDefaultText ? _InsufficientFundsText : customInsufficientFundsText;

            hasColorTransition = true;
        }

        protected override void onEnablePopUp()
        {
            base.onEnablePopUp();

            _BoxScaleAnimation = StartCoroutine(AnimateScale(_BoxImage.rectTransform, Vector3.one, _ScaleSpeed, null));
            
            transform.SetActiveChildren(true);

            _PanelPopUp.EnablePopUp();

            if(hasSufficientFunds)
                OnSufficientFunds();

            else
                OnInsufficientFunds();
        }

        protected override void onDisablePopUp()
        {
            base.onDisablePopUp();

            _PanelPopUp.DisablePopUp();

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
        }
    }
}