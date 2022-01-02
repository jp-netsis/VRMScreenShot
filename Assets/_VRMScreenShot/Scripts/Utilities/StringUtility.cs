using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace jp.netsis.VRMScreenShot.Utilities
{
public class StringUtility : MonoBehaviour
{
    [Serializable]
    public class String2ULongEvent : UnityEvent<ulong> {}
    [Serializable]
    public class String2IntEvent : UnityEvent<int> {}
    [Serializable]
    public class String2FloatEvent : UnityEvent<float> {}
    [Serializable]
    public class String2DoubleEvent : UnityEvent<double> {}

    [SerializeField]
    private String2ULongEvent _onString2Long;
    [SerializeField]
    private String2IntEvent _onString2Int;
    [SerializeField]
    private String2FloatEvent _onString2Float;
    [SerializeField]
    private String2DoubleEvent _onString2Double;

    public void String2ULong(string str)
    {
        ulong value;
        if(ulong.TryParse(str,out value))
        {
            _onString2Long?.Invoke(value);
        }
    }

    public void String2Int(string str)
    {
        int value;
        if(int.TryParse(str,out value))
        {
            _onString2Int?.Invoke(value);
        }
    }
    
    public void String2Float(string str)
    {
        float value;
        if(float.TryParse(str,out value))
        {
            _onString2Float?.Invoke(value);
        }
    }
    
    public void String2Double(string str)
    {
        double value;
        if(double.TryParse(str,out value))
        {
            _onString2Double?.Invoke(value);
        }
    }
}
}
