using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class CheckoutPopup : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField]
        Image _Panel;
        [SerializeField]
        float _PanelAlphaAnimationSpeed;
        [SerializeField]
        Image _PopupImage;
        [SerializeField]
        TMP_Text _Text;
        [SerializeField]
        Button _Button;

        Coroutine _PanelAlphaAnimation;

        public bool hasSufficientFunds { get; set; } = true;

        void Awake() 
        {
            _Panel.color = new Color(_Panel.color.r, _Panel.color.g, _Panel.color.b, 0);
        }

        IEnumerator AnimateTransparency(Image image, float targetAlpha, float delta)
        {
            while(image.color.a != targetAlpha)
            {
                image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, targetAlpha), delta * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        public void EnablePopUp()
        {
            _PanelAlphaAnimation = StartCoroutine(AnimateTransparency(_Panel, 0.5f, _PanelAlphaAnimationSpeed));
        }

        public void DisablePopUp()
        {
            if(_PanelAlphaAnimation != null)
                StopCoroutine(_PanelAlphaAnimation);
                
            _PanelAlphaAnimation = StartCoroutine(AnimateTransparency(_Panel, 0, _PanelAlphaAnimationSpeed));
        }

        public void ShowInsufficientFunds()
        {

        }

        public void ShowProcessingCheckout(System.Action onAfterLoad)
        {

        }
    }
}