using System.Collections.Generic;
using UnityEngine;

namespace VRMScreenShot
{
    [CreateAssetMenu(fileName = "VRMScreenShotScriptableObject",menuName = "VRMScreenShot/Create VRMScreenShotScriptableObject")]
    public class VRMScreenShotScriptableObject : ScriptableObject
    {
        [SerializeField]
        private AnimationClip[] _animationClipArray;

        public AnimationClip[] AnimationClipArray => _animationClipArray;
    }
}