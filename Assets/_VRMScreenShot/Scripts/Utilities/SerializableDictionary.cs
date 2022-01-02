using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace jp.netsis.VRMScreenShot.Utilities
{
    [Serializable]
    public abstract class SerializableDictionary<TKey, TValue, Type> where Type : KeyAndValuePair<TKey, TValue>
    {
        [SerializeField]
        private List<Type> _list;

        private Dictionary<TKey, TValue> _dic;

        public Dictionary<TKey, TValue> Get()
        {
            if (_dic == null)
            {
                _dic = List2Dic(_list);
            }

            return _dic;
        }

        public bool TryGetValue(TKey key,out TValue value)
        {
            var dic = Get();
            return dic.TryGetValue(key, out value);
        }

        static Dictionary<TKey, TValue> List2Dic(List<Type> list)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            foreach (var pair in list)
            {
                dic.Add(pair.Key,pair.Value);
            }
            return dic;
        }

    }
    [Serializable]
    public abstract class KeyAndValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public KeyAndValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    [Serializable]
    public class StringDictionary : SerializableDictionary<string, string, StringPair>
    {
    }

    [Serializable]
    public class StringPair : KeyAndValuePair<string, string>{
        public StringPair (string key, string value) : base (key, value) {}
    }
}