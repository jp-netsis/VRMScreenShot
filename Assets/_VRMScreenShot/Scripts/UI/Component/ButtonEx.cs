using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRMScreenShot.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonEx : MonoBehaviour, IPointerDownHandler
    {
        [Serializable]
        /// <summary>
        /// Function definition for a button click event.
        /// </summary>
        public class ButtonClickedEvent : UnityEvent<string> {}

        [SerializeField]
        private ButtonClickedEvent _OnClick = new ButtonClickedEvent();

        
        private Button _button;

#if UNITY_WEBGL && !UNITY_EDITOR
        public void OnPointerDown(PointerEventData eventData)
        {
            _OnClick?.Invoke(name);
        }
#else
        public void OnPointerDown(PointerEventData eventData) {}

        void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        void OnClick()
        {
            _OnClick?.Invoke(name);
        }
#endif
    }
}
