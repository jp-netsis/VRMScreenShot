using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace jp.netsis.VRMScreenShot
{
    [RequireComponent(typeof(Volume))]
    public class GIDataSetter : MonoBehaviour
    {
        [SerializeField]
        private CameraScriptableObject _cameraScriptableObject;

        private Volume _volume;

        private bool _initIsBloom;
        private float _initBloomThreshold;
        private float _initBloomIntensity;
        private Color _initBloomColor;
        private void OnEnable()
        {
            _initIsBloom = _cameraScriptableObject.IsBloom;
            _initBloomThreshold = _cameraScriptableObject.BloomThreshold;
            _initBloomIntensity = _cameraScriptableObject.BloomIntensity;
            _initBloomColor = _cameraScriptableObject.BloomColor;

            _volume = GetComponent<Volume>();
            OnValueChanged();
            _cameraScriptableObject.OnValueChanged += OnValueChanged;
        }

        void OnValueChanged()
        {
            var bloom = _volume.sharedProfile.components.Select(x => (Bloom)x).First();
            if (null != bloom)
            {
                bloom.active = _cameraScriptableObject.IsBloom;
                bloom.threshold.value = _cameraScriptableObject.BloomThreshold;
                bloom.intensity.value = _cameraScriptableObject.BloomIntensity;
                bloom.tint.value = _cameraScriptableObject.BloomColor;
            }
        }

        private void OnDisable()
        {
            _cameraScriptableObject.OnValueChanged -= OnValueChanged;
            
            _cameraScriptableObject.IsBloom = _initIsBloom;
            _cameraScriptableObject.BloomThreshold = _initBloomThreshold;
            _cameraScriptableObject.BloomIntensity = _initBloomIntensity;
            _cameraScriptableObject.BloomColor = _initBloomColor;
        }
    }
}