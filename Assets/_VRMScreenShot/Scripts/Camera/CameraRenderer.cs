using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.Rendering;

namespace VRMScreenShot
{
    public partial class CameraRenderer : MonoBehaviour
    {
        [SerializeField]
        private CameraScriptableObject _cameraScriptableObject;

        [SerializeField]
        private Camera _camera;

        private Vector2Int _renderSize = new Vector2Int(1024, 1024);

        private void OnEnable()
        {
            OnCameraScriptableObjectValueChanged();
            _cameraScriptableObject.OnValueChanged += OnCameraScriptableObjectValueChanged;
        }

        void OnCameraScriptableObjectValueChanged()
        {
            _renderSize = _cameraScriptableObject.ScreenShotSize;
        }

        private void OnDisable()
        {
            _cameraScriptableObject.OnValueChanged -= OnCameraScriptableObjectValueChanged;
        }

        /// <summary>
        /// Not BuckGround
        /// </summary>
        public void OnScreenShot()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine( OnScreenShotWebGL() );
            return;
#endif
            if (!SystemInfo.supportsAsyncGPUReadback)
            {
                Debug.LogWarning($"Cannot Support ScreenShot.");
            }

            var rt = RenderTexture.GetTemporary(_renderSize.x, _renderSize.y, 24, RenderTextureFormat.ARGB32);
            var oldTarget = _camera.targetTexture;
            _camera.targetTexture = rt;
            _camera.Render();
            _camera.targetTexture = oldTarget;

            // GPU上にあるピクセル情報を取得する
            AsyncGPUReadback.Request(rt, 0, request =>
            {
                if (request.hasError)
                {
                    // エラー
                    Debug.LogError("Error.");
                }
                else
                {
                    // データを取得してTexture2Dに反映する
                    var data = request.GetData<Color32>();
                    var texture = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
                    texture.LoadRawTextureData(data);
                    texture.Apply();
                    // PNG 画像としてファイル保存
                    StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "png", (string path) => { WritePngFile(path, texture); });

                    // 元のRenderTextureはもういらないので解放
                    RenderTexture.ReleaseTemporary(rt);
                }
            });
        }

        /// <summary>
        /// Add BuckGround
        /// </summary>
        public void OnPostRenderScreenShot()
        {
            StartCoroutine(PostRenderScreenShotCoroutine());
        }

        public IEnumerator PostRenderScreenShotCoroutine()
        {
            yield return new WaitForEndOfFrame();
            
            var texture = new Texture2D (_renderSize.x, _renderSize.y, TextureFormat.ARGB32, false);
            texture.ReadPixels ( new Rect(0, 0, _renderSize.x, _renderSize.y), 0, 0);
            texture.Apply ();

            // PNG 画像としてファイル保存
            StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "png", (string path) =>
            {
                WritePngFile(path, texture);
                Destroy (texture);
            });
        }

        void WritePngFile(string path, Texture2D texture)
        {
            if (string.IsNullOrEmpty(path))
            {
                return; // Dont Save File
            }
            // PNG Save
            File.WriteAllBytes(
                path,
                texture.EncodeToPNG());
        }

        static Texture2D ResizeTexture(Texture2D srcTexture, int newWidth, int newHeight)
        {
            var resizedTexture = new Texture2D(newWidth, newHeight);
            Graphics.ConvertTexture(srcTexture, resizedTexture);
            return resizedTexture;
        }
    }
}