using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMScreenShot
{
    [RequireComponent(typeof(Light))]
    public class LightDataSetter : MonoBehaviour
    {
        [SerializeField]
        private LightScriptableObject _lightScriptableObject;

        private Transform _transform;
        private Light _light;
        private void OnEnable()
        {
            _transform = transform;
            _light = GetComponent<Light>();
            OnValueChanged();
            _lightScriptableObject.OnValueChanged += OnValueChanged;
        }

        void OnValueChanged()
        {
            _transform.eulerAngles = _lightScriptableObject.Rotation;
            _light.intensity = _lightScriptableObject.Intensity;
            _light.color = _lightScriptableObject.LightColor;
        }

        private void OnDisable()
        {
            _lightScriptableObject.OnValueChanged -= OnValueChanged;
        }
    }
}