using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRM;

namespace jp.netsis.VRMScreenShot.UI
{
    public class EmotionList : MonoBehaviour
    {
        [SerializeField]
        private EmotionSlider _emotionSliderPrefab;
        [SerializeField]
        private EmoteScriptableObject _emoteScriptableObject;

        private List<EmotionSlider> _emotionSliderList = new List<EmotionSlider>();

        // Start is called before the first frame update
        void Start()
        {
            for (int n = _emotionSliderList.Count - 1; n >= 0; ++n)
            {
                _emotionSliderList[n].OnEmotionSliderValueChange.RemoveListener(OnEmotionSliderValueChange);
                Destroy(_emotionSliderList[n].gameObject);   
            }
            _emotionSliderList.Clear();
            foreach (var preset in Enum.GetValues(typeof(BlendShapePreset)))
            {
                string name = Enum.GetName(typeof(BlendShapePreset), preset);
                var emotionSlider = Instantiate(_emotionSliderPrefab,transform);
                emotionSlider.name = $"ES_{name}"; // ES : Emote Slider
                emotionSlider.SetValueWithoutNotify(0);
                emotionSlider.OnEmotionSliderValueChange.AddListener(OnEmotionSliderValueChange);
                string visibleName;
                if (LocalizationSelectorScriptableObject.Instance.TryGetValue(name, out visibleName))
                {
                    emotionSlider.SetName(name,visibleName);
                }
                _emotionSliderList.Add(emotionSlider);
            }
        }

        void OnEmotionSliderValueChange(string emotionName, float value)
        {
            _emoteScriptableObject.EmotionName = emotionName;
            _emoteScriptableObject.NormalizedTime = value;
        }
    }
}
