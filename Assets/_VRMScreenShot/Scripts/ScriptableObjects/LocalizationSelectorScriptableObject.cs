using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jp.netsis.VRMScreenShot.Utilities;

namespace jp.netsis.VRMScreenShot
{
    [CreateAssetMenu(fileName = "LocalizationSelectorScriptableObject", menuName = "VRMScreenShot/Create LocalizationSelectorScriptableObject")]
    public class LocalizationSelectorScriptableObject : SingletonScriptableObject<LocalizationSelectorScriptableObject>
    {
        [SerializeField]
        private List<LocalizationScriptableObject> _localizationList;

        private LocalizationScriptableObject _selectLocalizationScriptableObject;

        public void Initialize(SystemLanguage systemLanguage)
        {
            _selectLocalizationScriptableObject = GetLocalizationScriptableObject(systemLanguage);
        }

        string GetISOLanguageCode(SystemLanguage systemLanguage)
        {
            switch (systemLanguage)
            {
                case SystemLanguage.Japanese:
                    return "ja-JP";
            }
            return "en-US";
        }
        LocalizationScriptableObject GetLocalizationScriptableObject(SystemLanguage systemLanguage)
        {
            string soName = $"{GetISOLanguageCode(systemLanguage)}_LocalizationScriptableObject";
            Debug.Log($"{soName}");
            foreach (var localizationSo in _localizationList)
            {
                if (localizationSo.name == soName)
                {
                    return localizationSo;
                }
            }
            return _localizationList[0];
        }

        public bool TryGetValue(string key, out string value)
        {
            return _selectLocalizationScriptableObject.GetLocalizeString(key, out value);
        }
    }
}
