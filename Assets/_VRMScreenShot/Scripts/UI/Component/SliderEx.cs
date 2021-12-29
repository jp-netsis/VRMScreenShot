using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VRMScreenShot.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderEx : MonoBehaviour
    {
        [Serializable]
        public class SliderValueChangeEvent : UnityEvent<float> {}

        [SerializeField]
        private SliderValueChangeEvent _onSliderValueChange;

        public void OnSliderValueChange(float value)
        {
            _onSliderValueChange?.Invoke(value);
        }
    }
}