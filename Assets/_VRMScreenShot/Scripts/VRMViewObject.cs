using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using VRM;

namespace jp.netsis.VRMScreenShot
{
    public class VRMViewObject : MonoBehaviour
    {
        private VRMBlendShapeProxy _blendShapes;
        private SimpleAnimation _simpleAnimation;

        private VRMScreenShotScriptableObject _vrmScreenShotScriptableObject;
        private AnimationScriptableObject _animationScriptableObject;
        private EmoteScriptableObject _emoteScriptableObject;

        private bool _isInitialized;

        void OnEnable()
        {
            _blendShapes = GetComponent<VRMBlendShapeProxy>();
            _simpleAnimation = GetComponent<SimpleAnimation>();
        }

        /// <summary>
        /// After called OnEnable
        /// </summary>
        public void OnInit(VRMScreenShotScriptableObject vrmScreenShotScriptableObject,
            AnimationScriptableObject animationScriptableObject,
            EmoteScriptableObject emoteScriptableObject)
        {
            _vrmScreenShotScriptableObject = vrmScreenShotScriptableObject;
            _animationScriptableObject = animationScriptableObject;
            _emoteScriptableObject = emoteScriptableObject;
            if (null != _vrmScreenShotScriptableObject)
            {
                foreach (var animationClip in _vrmScreenShotScriptableObject.AnimationClipArray)
                {
                    _simpleAnimation.AddClip(animationClip,animationClip.name);
                }

                if (0 < _vrmScreenShotScriptableObject.AnimationClipArray.Length)
                {
                    var firstClip = _vrmScreenShotScriptableObject.AnimationClipArray[0];
                    OnReplay(firstClip.name);
                }
            }

            _isInitialized = true;
            _animationScriptableObject.OnValueChanged += OnAnimationValueChanged;
            _emoteScriptableObject.OnValueChanged += OnEmotionValueChanged;
        }

        private void OnDisable()
        {
            if (_isInitialized)
            {
                _isInitialized = false;
                _animationScriptableObject.OnValueChanged -= OnAnimationValueChanged;
                _emoteScriptableObject.OnValueChanged -= OnEmotionValueChanged;
            }
        }

        void OnAnimationValueChanged()
        {
            OnPause(_animationScriptableObject.AnimationName,_animationScriptableObject.NormalizedTime);
        }

        void OnEmotionValueChanged()
        {
            BlendShapePreset preset = BlendShapePreset.Unknown;
            if (BlendShapePreset.TryParse(_emoteScriptableObject.EmotionName, out preset))
            {
                SetShape(preset,_emoteScriptableObject.NormalizedTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animationName">Play Animation Name</param>
        /// <param name="normalizedTime">0(start)-1(end)</param>
        public void OnPause(string animationName, float normalizedTime)
        {
            _simpleAnimation.Play(animationName);
            var state = _simpleAnimation.GetState(animationName);
            state.speed = 0f;
            state.normalizedTime = normalizedTime;
        }

        public void OnReplay(string animationName)
        {
            _simpleAnimation.Play(animationName);
            var state = _simpleAnimation.GetState(animationName);
            state.speed = 1f;
        }
        
        public void SetShape(BlendShapePreset preset, float value)
        {
            _blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(preset), value);
        }
    }
}