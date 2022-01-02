using System;
using System.Collections;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace jp.netsis.VRMScreenShot
{
    [CreateAssetMenu(fileName = "OnColorChangedScriptableObject", menuName = "VRMScreenShot/Create OnColorChangedScriptableObject")]
    public class OnColorChangedScriptableObject : ScriptableObject
    {
        public UnityAction<Color> OnComplete;
        [CanBeNull]
        public UnityAction<Color,UnityAction<Color>> OnRaise;

        public void Raise(Color color,UnityAction<Color> onComplete)
        {
            OnRaise?.Invoke(color,onComplete);
        }
    }
}