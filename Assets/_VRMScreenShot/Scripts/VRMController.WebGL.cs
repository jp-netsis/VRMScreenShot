using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SFB;
using UniGLTF;
using UnityEngine;
using UnityEngine.Networking;
using VRM;

namespace jp.netsis.VRMScreenShot
{
    public partial class VRMController
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //
        // WebGL
        //
        [DllImport("__Internal")]
        private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

        public void OnOpenVRMWebGL()
        {
            UploadFile(gameObject.name, nameof(OnFileUpload), ".vrm", false);
        }
#endif

        public void OnFileUpload(string url)
        {
            StartCoroutine(LoadVRMFromUrl(url));
        }

        IEnumerator LoadVRMFromUrl(string url)
        {
            byte[] binary;

            using (UnityWebRequest uwr = UnityWebRequest.Get(url))
            {
                yield return uwr.SendWebRequest();
                binary = uwr.downloadHandler.data;
            }
            GltfData data;
            try
            {
                data = new GlbLowLevelParser(url, binary).Parse();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Parse Error {ex}");
                yield break;
            }

            LoadFromUrlAsync(data);
        }

        IEnumerator LoadFromUrl(string url)
        {
            byte[] binary;

            using (UnityWebRequest uwr = UnityWebRequest.Get(url))
            {
                yield return uwr.SendWebRequest();
                binary = uwr.downloadHandler.data;
            }

            GltfData data;
            try
            {
                data = new GlbLowLevelParser(url, binary).Parse();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Parse Error : {ex}");
                yield break;
            }

            LoadFromUrlAsync(data);
        }

        async void LoadFromUrlAsync(GltfData data)
        {
            try
            {
                var vrm = new VRMData(data);
                using (var loader = new VRMImporterContext(vrm, materialGenerator: new VRM.VRMMaterialDescriptorGenerator(vrm.VrmExtension)))
                {
                    var instance = await loader.LoadAsync();
                    SetModel(instance);
                }
            }
            catch (NotVrm0Exception)
            {
                // retry
                Debug.LogWarning("file extension is vrm. but not vrm ?");
                using (var loader = new UniGLTF.ImporterContext(data, materialGenerator: new GltfMaterialDescriptorGenerator()))
                {
                    var instance = await loader.LoadAsync();
                    SetModel(instance);
                }
            }
        }

    }
}