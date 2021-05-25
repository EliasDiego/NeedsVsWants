using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareSlider : MonoBehaviour
    {
        [SerializeField]
        float _ValueChangeSpeed = 1;
        
        bool _IsValueChanged = false;

        float _NewValue = 0;

        Slider _Slider;

        public float value 
        {
            get => _Slider.value;
            set 
            {
                _NewValue = value;

                _IsValueChanged = true;
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

        void Awake() 
        {
            _Slider = GetComponentInChildren<Slider>();
        }

        void Update()
        {
            if(_IsValueChanged)
            {
                _Slider.value = Mathf.MoveTowards(_Slider.value, _NewValue, _ValueChangeSpeed * Time.deltaTime);

                if(Mathf.Approximately(_Slider.value, _NewValue))
                    _IsValueChanged = false;
            }
        }
    }
}