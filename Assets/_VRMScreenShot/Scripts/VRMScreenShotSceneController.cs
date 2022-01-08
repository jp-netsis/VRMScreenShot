using System;
using System.Collections.Generic;
using UnityEngine;
using jp.netsis.VRMScreenShot.UI;

namespace jp.netsis.VRMScreenShot
{
    public class VRMScreenShotSceneController : MonoBehaviour
    {
        [SerializeField]
        private VRMController _vrmController;

        [SerializeField]
        private CameraRenderer _cameraRenderer;

        [SerializeField]
        private LightScriptableObject _lightScriptableObject;
        [SerializeField]
        private LightControl _lightControl;

        [SerializeField]
        private GameObject[] _tabUIArray;
        private Dictionary<string, Action> _dicTabButtonAction;

        private void OnEnable()
        {
            _dicTabButtonAction = new Dictionary<string, Action>()
            {
                {"AvatarButton",_vrmController.OnOpenVRM},
                {"EmoteButton",()=>OpenTab("EmotionScrollRect")},
                {"AnimationButton",()=>OpenTab("VRMAnimationList")},
                {"LightButton",()=>OpenTab("LightControl")},
                {"CameraButton",()=>OpenTab("CameraControl")},
            };
            _lightControl.OnInit(_lightScriptableObject);
        }

        public void OnClickTab(string buttonName)
        {
            Action action;
            if (_dicTabButtonAction.TryGetValue(buttonName, out action))
            {
                action.Invoke();
            }
        }

        void OpenTab(string tabUIName)
        {
            bool isShow = false;
            foreach (var go in _tabUIArray)
            {
                isShow = false;
                if (go.name == tabUIName && go.activeSelf == false)
                {
                    isShow = true;
                }
                go.SetActive(isShow);
            }
        }

        public void OnClickScreenShot()
        {
            // In Back Ground, PostRender
            // Alpha Back Ground, Camera Render
            _cameraRenderer.OnScreenShot();
        }

    }
}