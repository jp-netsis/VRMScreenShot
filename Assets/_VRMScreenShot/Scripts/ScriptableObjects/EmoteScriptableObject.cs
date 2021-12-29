using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace VRMScreenShot
{
    [CreateAssetMenu(fileName = "EmoteScriptableObject", menuName = "VRMScreenShot/Create EmoteScriptableObject")]
    public class EmoteScriptableObject : OnValueChangedScriptableObject
    {
        [SerializeField]
        string _emotion = string.Empty;
        [SerializeField]
        private float _normalizedTime = 0f;
        
        public string EmotionName
        {
            get => _emotion;
            set
            {
                if (_emotion == value) return;
                _emotion = value;
                SetDirty();
            }
        }

        public float NormalizedTime
        {
            get => _normalizedTime;
            set
            {
                if(Mathf.Approximately(_normalizedTime, value)) return;
                _normalizedTime = value;
                SetDirty();
            } 
        }

        private void OnEnable()
        {
            _emotion = string.Empty;
            _normalizedTime = 0f;
        }
    }
}