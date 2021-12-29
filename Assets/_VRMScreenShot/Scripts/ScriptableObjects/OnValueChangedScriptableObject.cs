using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace VRMScreenShot
{
    public abstract class OnValueChangedScriptableObject : ScriptableObject
    {
        public UnityAction OnValueChanged;

        protected bool _isDirty;
        public bool IsDirty => _isDirty;

        protected CancellationTokenSource _cancellationTokenSource = null;

        protected virtual void SetDirty()
        {
            _isDirty = true;
        }

        public virtual void OnUpdate()
        {
            if (_isDirty)
            {
                _isDirty = false;
                OnValueChanged?.Invoke();
            }
        }
    }
}