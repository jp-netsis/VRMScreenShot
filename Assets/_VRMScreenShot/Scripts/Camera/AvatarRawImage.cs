using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace jp.netsis.VRMScreenShot
{
    [RequireComponent(typeof(RawImage))]
    public class AvatarRawImage : MonoBehaviour
    {
        [SerializeField]
        private Camera _avatarCamera;

        private UniversalAdditionalCameraData _universalAdditionalCameraData;
        private RawImage _rawImage; 
        private RenderTexture _renderTexture;
        private void OnEnable()
        {
            _rawImage = GetComponent<RawImage>();
            GenerateRenderTexture();
        }

        void GenerateRenderTexture()
        {
            _renderTexture = new RenderTexture(Screen.width,Screen.height,32, GraphicsFormat.R16G16B16A16_SFloat);
            _rawImage.texture = _renderTexture;
            _universalAdditionalCameraData = _avatarCamera.GetUniversalAdditionalCameraData();
            _avatarCamera.targetTexture = _renderTexture;
        }

        private void LateUpdate()
        {
            if (_renderTexture.width != Screen.width || _renderTexture.height != Screen.height)
            {
                SafeDestroyRenderTexture();
                GenerateRenderTexture();
            }
        }

        private void OnDisable()
        {
            SafeDestroyRenderTexture();
        }

        void SafeDestroyRenderTexture()
        {
            Destroy(_renderTexture);
            _renderTexture = null;
        }
    }
}