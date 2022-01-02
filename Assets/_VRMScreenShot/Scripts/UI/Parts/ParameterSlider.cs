using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace jp.netsis.VRMScreenShot.UI
{
    public class ParameterSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TMP_Text _parameterNameTMP;
        [SerializeField]
        private TMP_InputField _inputFieldTMP;

        public void SetValueWithoutNotify(float value)
        {
            _slider.SetValueWithoutNotify(value);
            _inputFieldTMP.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetName(string name)
        {
            _parameterNameTMP.text = name;
        }
    }
}