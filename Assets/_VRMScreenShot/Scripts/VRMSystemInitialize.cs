using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jp.netsis.VRMScreenShot
{
    public class VRMSystemInitialize : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            LocalizationSelectorScriptableObject.Instance.Initialize(Application.systemLanguage);
        }
    }
}