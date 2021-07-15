using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareHandler : MonoBehaviour
    {
        [SerializeField]
        Slider _Slider;
        [SerializeField]
        float _ValueChangeSpeed = 1;
        [SerializeField]
        WelfareEmote _Emote;
        
        bool _IsValueChanged = false;

        float _NewValue = 0;

        public float value 
        {
            get => _Slider.value;
            set 
            {
                _NewValue = value;

                _IsValueChanged = true;
                
                if(_Emote)
                {
                    _Emote.isPositive = _Slider.value < value;
                    _Emote?.DisablePopUp();
                }
            }
        }

        public float maxValue { get => _Slider.maxValue; set => _Slider.maxValue = value; }

        public WelfareValue welfareValue 
        {
            get => new WelfareValue(_NewValue, maxValue);
            set
            {
                _Slider.maxValue = value.maxValue;
                _Slider.value = value.value;
            }
        }

        void Update()
        {
            if(_IsValueChanged)
            {
                _Slider.value = Mathf.MoveTowards(_Slider.value, _NewValue, _ValueChangeSpeed * Time.deltaTime * CalendarSystem.DayProgressor.hourTimeScale);

                if(Mathf.Approximately(_Slider.value, _NewValue))
                    _IsValueChanged = false;
            }
        }
    }
}