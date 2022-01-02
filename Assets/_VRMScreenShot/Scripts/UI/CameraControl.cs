using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRM;

namespace jp.netsis.VRMScreenShot.UI
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField]
        private CameraScriptableObject _cameraScriptableObject;

        [SerializeField]
        private TMP_InputField _shotSizeX;
        [SerializeField]
        private TMP_InputField _shotSizeY;

        [SerializeField]
        private Image _bgColor1;
        [SerializeField]
        private Image _bgColor2;
        [SerializeField]
        private ParameterSlider _bgRotSlider;

        [SerializeField]
        private TMP_InputField _positionX;
        [SerializeField]
        private TMP_InputField _positionY;
        [SerializeField]
        private TMP_InputField _positionZ;

        [SerializeField]
        private TMP_InputField _rotationX;
        [SerializeField]
        private TMP_InputField _rotationY;
        [SerializeField]
        private TMP_InputField _rotationZ;

        [SerializeField]
        private Toggle _isOrthographicToggle;

        [SerializeField]
        private ParameterSlider _zoomSlider;

        [SerializeField]
        private ParameterSlider _fovSlider;

        [SerializeField]
        private ParameterSlider _orthoSizeSlider;

        [SerializeField]
        private Toggle _isBloomToggle;
        [SerializeField]
        private ParameterSlider _bloomThresholdSlider;
        [SerializeField]
        private ParameterSlider _bloomIntensitySlider;
        [SerializeField]
        private Image _bloomColor;

        private Vector3 _initPos;
        private Vector3 _initRot;

        private void OnEnable()
        {
            _initPos = _cameraScriptableObject.Position;
            _initRot = _cameraScriptableObject.Rotation;
            OnValueChanged();
            _cameraScriptableObject.OnValueChanged += OnValueChanged;
        }

        /// <summary>
        /// From External UI
        /// </summary>
        void OnValueChanged()
        {
            _shotSizeX.SetTextWithoutNotify(_cameraScriptableObject.ScreenShotSize.x.ToString());
            _shotSizeY.SetTextWithoutNotify(_cameraScriptableObject.ScreenShotSize.y.ToString());
            _bgColor1.color = _cameraScriptableObject.Bg1Color;
            _bgColor2.color = _cameraScriptableObject.Bg2Color;
            _bgRotSlider.SetValueWithoutNotify(_cameraScriptableObject.BgRot);
            _positionX.SetTextWithoutNotify(_cameraScriptableObject.Position.x.ToString(CultureInfo.InvariantCulture));
            _positionY.SetTextWithoutNotify(_cameraScriptableObject.Position.y.ToString(CultureInfo.InvariantCulture));
            _positionZ.SetTextWithoutNotify(_cameraScriptableObject.Position.z.ToString(CultureInfo.InvariantCulture));
            _rotationX.SetTextWithoutNotify(_cameraScriptableObject.Rotation.x.ToString(CultureInfo.InvariantCulture));
            _rotationY.SetTextWithoutNotify(_cameraScriptableObject.Rotation.y.ToString(CultureInfo.InvariantCulture));
            _rotationZ.SetTextWithoutNotify(_cameraScriptableObject.Rotation.z.ToString(CultureInfo.InvariantCulture));
            _isOrthographicToggle.SetIsOnWithoutNotify(_cameraScriptableObject.IsOrtho);
            _zoomSlider.SetValueWithoutNotify(_cameraScriptableObject.Zoom);
            _fovSlider.SetValueWithoutNotify(_cameraScriptableObject.FOV);
            _orthoSizeSlider.SetValueWithoutNotify(_cameraScriptableObject.OrthoSize);
            _isBloomToggle.SetIsOnWithoutNotify(_cameraScriptableObject.IsBloom);
            _bloomThresholdSlider.SetValueWithoutNotify(_cameraScriptableObject.BloomThreshold);
            _bloomIntensitySlider.SetValueWithoutNotify(_cameraScriptableObject.BloomIntensity);
            _bloomColor.color = _cameraScriptableObject.BloomColor;
        }

        private void OnDisable()
        {
            _cameraScriptableObject.OnValueChanged -= OnValueChanged;
        }

        public void ResetPosition()
        {
            _cameraScriptableObject.Position = _initPos;
        }

        public void ResetRotation()
        {
            _cameraScriptableObject.Rotation = _initRot;
        }
    }
}
