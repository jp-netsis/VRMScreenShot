using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRM;

namespace VRMScreenShot.UI
{
    public class LightControl : MonoBehaviour
    {
        [SerializeField]
        private LightScriptableObject _lightScriptableObject;
        
        [SerializeField]
        private TMP_InputField _rotationX;
        [SerializeField]
        private TMP_InputField _rotationY;
        [SerializeField]
        private TMP_InputField _rotationZ;
        [SerializeField]
        private ParameterSlider _intensitySlider;
        [SerializeField]
        private Image _lightColor;


        public void OnInit(LightScriptableObject lightScriptableObject)
        {
            _lightScriptableObject = lightScriptableObject;
            OnValueChanged();
            _lightScriptableObject.OnValueChanged += OnValueChanged;
        }

        /// <summary>
        /// From External UI
        /// </summary>
        void OnValueChanged()
        {
            _rotationX.SetTextWithoutNotify(_lightScriptableObject.Rotation.x.ToString(CultureInfo.InvariantCulture));
            _rotationY.SetTextWithoutNotify(_lightScriptableObject.Rotation.y.ToString(CultureInfo.InvariantCulture));
            _rotationZ.SetTextWithoutNotify(_lightScriptableObject.Rotation.z.ToString(CultureInfo.InvariantCulture));
            _intensitySlider.SetValueWithoutNotify(_lightScriptableObject.Intensity);
            _lightColor.color = _lightScriptableObject.LightColor;
        }
        private void OnDisable()
        {
            _lightScriptableObject.OnValueChanged -= OnValueChanged;
        }

        
    }
}
