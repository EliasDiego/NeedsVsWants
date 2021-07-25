using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace NeedsVsWants.TooltipSystems
{
    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] InputActionReference HoverPoint;

        public TextMeshProUGUI headerField;

        public TextMeshProUGUI contentField;

        public LayoutElement layoutElement;

        public int characterWrapLimit;

        public RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetText(string content, string header = "")
        {
            if(string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }

            contentField.text = content;

            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

        }
        private void Update()
        {
            if (Application.isEditor)
            {
                int headerLength = headerField.text.Length;
                int contentLength = contentField.text.Length;

                layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
            }

            transform.position = HoverPoint.action.ReadValue<Vector2>();

            float pivotX = HoverPoint.action.ReadValue<Vector2>().x/Screen.width;
            float pivotY = HoverPoint.action.ReadValue<Vector2>().y/Screen.height;
            rectTransform.pivot = new Vector2(pivotY, pivotX);
            
        }

        
        

    }
}