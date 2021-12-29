using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMScreenShot
{
    [RequireComponent(typeof(Camera))]
    public class CameraDataSetter : MonoBehaviour
    {
        [SerializeField]
        private CameraScriptableObject _cameraScriptableObject;
        [SerializeField]
        private Camera _bgCamera;
        [SerializeField]
        private Transform _cameraTransformRoot;

        private Camera _camera;

        private void OnEnable()
        {
            _camera = GetComponent<Camera>();
            OnValueChanged();
            _cameraScriptableObject.OnValueChanged += OnValueChanged;
        }

        void OnValueChanged()
        {
            _cameraTransformRoot.position = _cameraScriptableObject.Position + Quaternion.Euler(_cameraScriptableObject.Rotation) * new Vector3(0,0,_cameraScriptableObject.Zoom);
            _cameraTransformRoot.eulerAngles = _cameraScriptableObject.Rotation;
            _camera.fieldOfView = _cameraScriptableObject.FOV;
            _camera.orthographic = _cameraScriptableObject.IsOrtho;
            _camera.orthographicSize = _cameraScriptableObject.OrthoSize;

            RenderSettings.skybox.SetColor(CameraScriptableObject.ColorStart,_cameraScriptableObject.Bg1Color);
            RenderSettings.skybox.SetColor(CameraScriptableObject.ColorEnd,_cameraScriptableObject.Bg2Color);
            RenderSettings.skybox.SetFloat(CameraScriptableObject.Angle,_cameraScriptableObject.BgRot * Mathf.Deg2Rad);
        }

        private void OnDisable()
        {
            _cameraScriptableObject.OnValueChanged -= OnValueChanged;
        }
    }
}