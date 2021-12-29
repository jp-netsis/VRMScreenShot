using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

namespace VRMScreenShot.Utilities
{
    public class ValueUtility : MonoBehaviour
    {
        [Serializable]
        public class StringEvent : UnityEvent<string> {}

        [SerializeField]
        private StringEvent _onCastString;

        [SerializeField]
        private UnityEvent _onBoolEnable;

        [SerializeField]
        private UnityEvent _onBoolDisable;

        public void ULong2String(ulong value) => _onCastString?.Invoke(value.ToString());
        public void Int2String(int value) => _onCastString?.Invoke(value.ToString());
        public void Float2String(float value) => _onCastString?.Invoke(value.ToString(CultureInfo.InvariantCulture));
        public void Double2String(double value) => _onCastString?.Invoke(value.ToString(CultureInfo.InvariantCulture));

        public void BoolCallback(bool value)
        {
            if (value)
            {
                _onBoolEnable?.Invoke();
            }
            else
            {
                _onBoolDisable?.Invoke();
            }
        }
    }
}
