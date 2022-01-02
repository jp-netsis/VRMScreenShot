using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace jp.netsis.VRMScreenShot
{
    [CreateAssetMenu(fileName = "AnimationScriptableObject", menuName = "VRMScreenShot/Create AnimationScriptableObject")]
    public class AnimationScriptableObject : OnValueChangedScriptableObject
    {
        [SerializeField]
        string _animation = string.Empty;
        [SerializeField]
        private float _normalizedTime = 0f;
        
        public string AnimationName
        {
            get => _animation;
            set
            {
                if (_animation == value) return;
                _animation = value;
                SetDirty();
            }
        }

        public float NormalizedTime
        {
            get => _normalizedTime;
            set
            {
                if (Mathf.Approximately(_normalizedTime,value)) return;
                _normalizedTime = value;
                SetDirty();
            } 
        }
    }
}