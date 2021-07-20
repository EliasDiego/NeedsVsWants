using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeedsVsWants.TooltipSystems
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string content;
        public string header;

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipSystem.Show(content, header);
        }

        
    }
}
