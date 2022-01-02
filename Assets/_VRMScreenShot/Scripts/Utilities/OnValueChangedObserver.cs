using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jp.netsis.VRMScreenShot.Utilities
{
    public class OnValueChangedObserver : MonoBehaviour
    {
        [SerializeField]
        private OnValueChangedScriptableObject[] _onValueChangedScriptableObjectArray;

        private void Update()
        {
            foreach (var so in _onValueChangedScriptableObjectArray)
            {
                if (so.IsDirty)
                {
                    so.OnUpdate();// IsDirty false
                }
            }
        }
    }
}