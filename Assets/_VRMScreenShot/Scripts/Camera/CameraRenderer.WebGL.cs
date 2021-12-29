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
    public partial class CameraRenderer
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //
        // WebGL
        //
        [DllImport("__Internal")]
        private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);
        
        public void OnFileDownload()
        {
            // download Screenshot success
            Debug.Log("Download Screenshot success");
        }
#endif

        IEnumerator OnScreenShotWebGL()
        {
            yield return new WaitForEndOfFrame();
            var rt = RenderTexture.GetTemporary(_renderSize.x, _renderSize.y, 24, RenderTextureFormat.ARGB32);
            var currentRT = RenderTexture.active;
            var oldTarget = _camera.targetTexture;
            _camera.targetTexture = rt;
            RenderTexture.active = _camera.targetTexture;
            _camera.Render();
            var texture = new Texture2D(_renderSize.x, _renderSize.y, TextureFormat.RGBA32, false);
            texture.ReadPixels(new Rect(0, 0, _renderSize.x, _renderSize.y), 0, 0);
            texture.Apply();
            _camera.targetTexture = oldTarget;
            RenderTexture.active = currentRT;
            var textureBytes = texture.EncodeToPNG();
#if UNITY_WEBGL && !UNITY_EDITOR
            DownloadFile(gameObject.name, "OnFileDownload", "screenshot.png", textureBytes, textureBytes.Length);
#endif
        }
    }
}
