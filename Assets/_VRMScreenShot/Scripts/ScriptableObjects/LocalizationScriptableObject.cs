using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jp.netsis.VRMScreenShot.Utilities;

namespace jp.netsis.VRMScreenShot
{
    [CreateAssetMenu(fileName = "LocalizationScriptableObject", menuName = "VRMScreenShot/Create LocalizationScriptableObject")]
    public class LocalizationScriptableObject : ScriptableObject
    {
        [SerializeField]
        private StringDictionary _localizationDictionary;

        public bool GetLocalizeString(string key, out string value)
        {
            if (_localizationDictionary.TryGetValue(key, out value))
            {
                return true;
            }
            return false;
        }
    }
}
