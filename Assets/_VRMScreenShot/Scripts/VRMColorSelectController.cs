using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using HSVPicker;
using SFB;
using UniGLTF;
using UnityEngine;
using UnityEngine.Events;
using VRM;
using VRMScreenShot.UI;

namespace VRMScreenShot
{
    public class VRMColorSelectController : MonoBehaviour
    {
        [SerializeField]
        private OnColorChangedScriptableObject _onColorChangedScriptableObject;

        [SerializeField]
        private GameObject _colorPickRoot;
        
        [SerializeField]
        private ColorPicker _colorPicker;

        private UnityAction<Color> _onComplete;

        private Color _initColor;

        private void OnEnable()
        {
            _onColorChangedScriptableObject.OnRaise += OnColorChangeRaise;
        }

        public void OnColorChangeRaise(Color color, UnityAction<Color> onComplete)
        {
            _initColor = color;
            _colorPicker.CurrentColor = _initColor;
            _colorPickRoot.SetActive(true);
            _onComplete = onComplete;
        }

        public void OnCancelPicker()
        {
            _onComplete?.Invoke(_initColor);
        }

        public void OnApplyPicker()
        {
            _onComplete?.Invoke(_colorPicker.CurrentColor);
        }

        private void OnDisable()
        {
            _onColorChangedScriptableObject.OnRaise -= OnColorChangeRaise;
        }
    }
}