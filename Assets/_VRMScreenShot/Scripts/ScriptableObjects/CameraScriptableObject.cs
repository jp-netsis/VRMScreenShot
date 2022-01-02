using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace jp.netsis.VRMScreenShot
{
    [CreateAssetMenu(fileName = "CameraScriptableObject", menuName = "VRMScreenShot/Create CameraScriptableObject")]
    public class CameraScriptableObject : OnValueChangedScriptableObject
    {
        public const string ColorStart = "_ColorStart";
        public const string ColorEnd = "_ColorEnd";
        public const string Angle = "_Angle";
        
        [SerializeField]
        private Vector2Int _screenShotSize = new Vector2Int(1280,720);
        [SerializeField]
        private Color _bg1;
        [SerializeField]
        private Color _bg2;
        [SerializeField]
        private float _bgRot;
        [SerializeField]
        private Vector3 _position;
        [SerializeField]
        private Vector3 _rotation;
        [SerializeField]
        private float _zoom;
        [SerializeField]
        private Vector3 _lookat;
        [SerializeField]
        private float _fov;
        [SerializeField]
        private bool _isOrtho;
        [SerializeField]
        private float _orthoSize;
        [SerializeField]
        private bool _isBloom;
        [SerializeField]
        private float _bloomThreshold;
        [SerializeField]
        private float _bloomIntensity;
        [SerializeField]
        private Color _bloomColor;

        public Vector2Int ScreenShotSize
        {
            get => _screenShotSize;
            set
            {
                if (_screenShotSize == value) return;
                _screenShotSize = value;
                SetDirty();
            }
        }

        public void SetScreenShotSizeX(int x)
        {
            var ssSize = ScreenShotSize;
            ssSize.x = x;
            ScreenShotSize = ssSize;
        }
        public void SetScreenShotSizeY(int y)
        {
            var ssSize = ScreenShotSize;
            ssSize.y = y;
            ScreenShotSize = ssSize;
        }

        public Color Bg1Color
        {
            get => _bg1;
            set
            {
                if (_bg1 == value) return;
                _bg1 = value;
                SetDirty();
            }
        }

        public Color Bg2Color
        {
            get => _bg2;
            set
            {
                if (_bg2 == value) return;
                _bg2 = value;
                SetDirty();
            }
        }

        public float BgRot
        {
            get => _bgRot;
            set
            {
                if (Mathf.Approximately(_bgRot,value)) return;
                _bgRot = value;
                SetDirty();
            }
        }
        
        public Vector3 Position
        {
            get => _position;
            set
            {
                if (_position == value) return;
                _position = value;
                SetDirty();
            }
        }

        public void SetPositionX(float x)
        {
            var p = Position;
            p.x = x;
            Position = p;
        }
        public void SetPositionY(float y)
        {
            var p = Position;
            p.y = y;
            Position = p;
        }
        public void SetPositionZ(float z)
        {
            var p = Position;
            p.z = z;
            Position = p;
        }
        
        public Vector3 Rotation
        {
            get => _rotation;
            set
            {
                if (_rotation == value) return;
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

        public Vector3 LookAt
        {
            get => _lookat;
            set
            {
                if (_lookat == value) return;
                _lookat = value;
                SetDirty();
            }
        }

        public float Zoom
        {
            get => _zoom;
            set
            {
                if (Mathf.Approximately(_zoom,value)) return;
                _zoom = value;
                SetDirty();
            }
        }
        
        public float FOV
        {
            get => _fov;
            set
            {
                if (Mathf.Approximately(_fov,value)) return;
                _fov = value;
                SetDirty();
            }
        }
        
        public bool IsOrtho
        {
            get => _isOrtho;
            set
            {
                if (_isOrtho == value) return;
                _isOrtho = value;
                SetDirty();
            }
        }
        
        public float OrthoSize
        {
            get => _orthoSize;
            set
            {
                if (Mathf.Approximately(_orthoSize,value)) return;
                _orthoSize = value;
                SetDirty();
            }
        }
        
        public bool IsBloom
        {
            get => _isBloom;
            set
            {
                if (_isBloom == value) return;
                _isBloom = value;
                SetDirty();
            }
        }
        
        public float BloomThreshold
        {
            get => _bloomThreshold;
            set
            {
                if (Mathf.Approximately(_bloomThreshold,value)) return;
                _bloomThreshold = value;
                SetDirty();
            }
        }

        public float BloomIntensity
        {
            get => _bloomIntensity;
            set
            {
                if (Mathf.Approximately(_bloomIntensity,value)) return;
                _bloomIntensity = value;
                SetDirty();
            }
        }
        
        public Color BloomColor
        {
            get => _bloomColor;
            set
            {
                if (_bloomColor == value) return;
                _bloomColor = value;
                SetDirty();
            }
        }
    }
}