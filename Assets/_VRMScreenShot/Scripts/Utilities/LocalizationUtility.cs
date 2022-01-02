using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace jp.netsis.VRMScreenShot.Utilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationUtility : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            string localizationName;
            if (LocalizationSelectorScriptableObject.Instance.TryGetValue(_text.text, out localizationName))
            {
                _text.text = localizationName;
            }
            else
            {
                Debug.LogWarning($"Not Found LocalizationKey => {_text.text}");
            }
        }
    }
}