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
        string _Id;
        [SerializeField]
        FloatMinMax _SliderMinMax = new FloatMinMax(0, 1);
        [SerializeField]
        float _ValueChangeSpeed = 1;
        
        bool _IsValueChanged = false;

        float _NewValue = 0;

        Slider _Slider;

        public string id => _Id;

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
        public float minValue { get => _Slider.minValue; set => _Slider.minValue = value; }

        void Awake() 
        {
            _Slider = GetComponentInChildren<Slider>();

            maxValue = _SliderMinMax.max;
            minValue = _SliderMinMax.min;
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