using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace jp.netsis.VRMScreenShot.UI
{
    public class AnimationSlider : MonoBehaviour
    {
        [SerializeField]
        private AnimationScriptableObject _animationScriptableObject;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TMP_InputField _inputFieldTMP;

        private string _animationName;
        
        public class AnimationSliderValueChangeEvent : UnityEvent<string,float> {}
        public AnimationSliderValueChangeEvent OnAnimationSliderValueChange = new AnimationSliderValueChangeEvent();

        public void SetValueWithoutNotify(float value)
        {
            _slider.SetValueWithoutNotify(value);
            _inputFieldTMP.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));
        }
        
        public void OnSliderValueChanged(float value)
        {
            OnAnimationSliderValueChange?.Invoke(_animationName,value);
        }

    }
}