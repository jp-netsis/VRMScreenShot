using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VRMScreenShot.UI
{
    public class EmotionSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TMP_Text _emotionNameTMP;
        [SerializeField]
        private TMP_InputField _inputFieldTMP;

        private string _emotionName;
        
        public class EmotionSliderValueChangeEvent : UnityEvent<string,float> {}
        public EmotionSliderValueChangeEvent OnEmotionSliderValueChange = new EmotionSliderValueChangeEvent();

        public void SetValueWithoutNotify(float value)
        {
            _slider.SetValueWithoutNotify(value);
            _inputFieldTMP.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetName(string emotionName,string vlsibleName)
        {
            _emotionName = emotionName;
            _emotionNameTMP.text = vlsibleName;
        }

        public void OnSliderValueChanged(float value)
        {
            OnEmotionSliderValueChange?.Invoke(_emotionName,value);
        }
    }
}