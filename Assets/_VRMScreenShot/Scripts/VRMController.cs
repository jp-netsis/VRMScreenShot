using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SFB;
using UniGLTF;
using UnityEngine;
using VRM;
using jp.netsis.VRMScreenShot.UI;

namespace jp.netsis.VRMScreenShot
{
    public partial class VRMController : MonoBehaviour
    {
        [SerializeField]
        private VRMScreenShotScriptableObject _vrmScreenShotScriptableObject;

        [SerializeField]
        private AnimationScriptableObject _animationScriptableObject;

        [SerializeField]
        private EmoteScriptableObject _emoteScriptableObject;

        [SerializeField]
        private VRMAnimationList _animationList;

        [SerializeField]
        private Transform _vrmRoot;

        private RuntimeGltfInstance _runtimeGltfInstance;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _animationList.OnInit(_vrmScreenShotScriptableObject,_animationScriptableObject);
        }

        void SafeDeleteRuntimeGltfInstance()
        {
            if (null != _runtimeGltfInstance)
            {
                var simpleAnimation = _runtimeGltfInstance.Root.GetComponent<SimpleAnimation>();
                if (null != simpleAnimation)
                {
                    Destroy(simpleAnimation);
                }
                var vrmViewerObject = _runtimeGltfInstance.Root.GetComponent<VRMViewObject>();
                if (null != vrmViewerObject)
                {
                    Destroy(vrmViewerObject);
                }
                _runtimeGltfInstance.Dispose();
                _runtimeGltfInstance = null;
            }
        }

        private void OnDisable()
        {
            SafeDeleteRuntimeGltfInstance();
            if (null != _cancellationTokenSource)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public void OnOpenVRM()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            OnOpenVRMWebGL();
#else
            StandaloneFileBrowser.OpenFilePanelAsync("Open VRM","","vrm",false, OnOpenVRMCallback);
#endif
        }

        void OnOpenVRMCallback(string[] paths)
        {
            LoadVRMAsync(paths,_cancellationTokenSource.Token);
        }
        
        async void LoadVRMAsync(string[] paths, CancellationToken token)
        {
            if (paths.Length == 0)
            {
                return; //  何も選択しなかった
            }
            if (paths.Length > 1)
            {
                Debug.LogWarning($"Only one VRM file can be selected at a time.");
                return;
            }

            var path = paths[0];
            if (!File.Exists(path))
            {
                Debug.LogWarning($"File does not exist.");
                return;
            }

            GltfData data;
            try
            {
                data = new AutoGltfFileParser(path).Parse();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Parse Error : {ex}");
                return;
            }

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

        void SetModel(RuntimeGltfInstance instance)
        {
            // If there is a model that has been loaded previously, delete it.
            SafeDeleteRuntimeGltfInstance();
            _runtimeGltfInstance = instance;
            _runtimeGltfInstance.EnableUpdateWhenOffscreen();
            _runtimeGltfInstance.ShowMeshes();
            _runtimeGltfInstance.Root.transform.SetParent(_vrmRoot,false);

            var simpleAnimation = _runtimeGltfInstance.Root.AddComponent<SimpleAnimation>();
            var vrmViewerObject = _runtimeGltfInstance.Root.AddComponent<VRMViewObject>();
            vrmViewerObject.OnInit(_vrmScreenShotScriptableObject,_animationScriptableObject,_emoteScriptableObject);

            StartCoroutine(InitializationVRM());
        }

        IEnumerator InitializationVRM()
        {
            yield return null; // wait 1F, can set proxy
            _emoteScriptableObject.OnValueChanged();
            _animationScriptableObject.OnValueChanged();
        }

    }
}