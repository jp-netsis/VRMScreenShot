using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using VRM;

namespace VRMScreenShot.UI
{
    public class VRMAnimationList : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown _animationDropdown;

        private VRMScreenShotScriptableObject _vrmScreenShotScriptableObject;
        private AnimationScriptableObject _animationScriptableObject;

        private List<string> _animationClipNameList = new List<string>();

        // Start is called before the first frame update
        public void OnInit(VRMScreenShotScriptableObject vrmScreenShotScriptableObject,AnimationScriptableObject animationScriptableObject)
        {
            _vrmScreenShotScriptableObject = vrmScreenShotScriptableObject;
            _animationScriptableObject = animationScriptableObject;
            
            List<string> optionlist = new List<string>();
            _animationClipNameList.Clear();

            foreach (var animClip in _vrmScreenShotScriptableObject.AnimationClipArray)
            {
                optionlist.Add(animClip.name);
                _animationClipNameList.Add(animClip.name);
            }

            if (0 < _animationClipNameList.Count)
            {
                _animationScriptableObject.AnimationName = _animationClipNameList[0];
            }
            _animationDropdown.ClearOptions();
            _animationDropdown.AddOptions(optionlist);
        }

        public void OnChangeAnimation(int index)
        {
            Debug.Log($"OnChangeAnimation : {_animationDropdown.options[index].text}");
            _animationScriptableObject.AnimationName = _animationClipNameList[index];
        }

        public void OnChangeAnimationSlider(float value)
        {
            _animationScriptableObject.NormalizedTime = value;
        }
    }
}
