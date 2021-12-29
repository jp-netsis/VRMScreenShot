using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace VRMScreenShot
{
    [CreateAssetMenu(fileName = "LightScriptableObject", menuName = "VRMScreenShot/Create LightScriptableObject")]
    public class LightScriptableObject : OnValueChangedScriptableObject
    {
        [SerializeField]
        Vector3 _rotation = Vector3.zero;

        public Vector3 Rotation
        {
            get => _rotation;
            set
            {
                if(_rotation == value) return;
                _rotation = value;
                SetDirty();
            }
        }

        public void SetRotationX(float x)
        {
            var r = Rotation;
            r.x = x;
            Rotation = r;
        }
        public void SetRotationY(float y)
        {
            var r = Rotation;
            r.y = y;
            Rotation = r;
        }
        public void SetRotationZ(float z)
        {
            var r = Rotation;
            r.z = z;
            Rotation = r;
        }

        [SerializeField]
        private float _intensity = 1f;

        public float Intensity
        {
            get => _intensity;
            set
            {
                if (Mathf.Approximately(_intensity,value)) return;
                _intensity = value;
                SetDirty();
            } 
        }

        [SerializeField]
        private Color _lightColor = Color.white;
        public Color LightColor
        {
            get => _lightColor;
            set
            {
                if(_lightColor == value) return;
                _lightColor = value;
                SetDirty();
            }
        }
    }
}