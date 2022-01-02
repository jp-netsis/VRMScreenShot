using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace jp.netsis.VRMScreenShot.UI
{
    [RequireComponent(typeof(Button))]
    public class ColorButton : MonoBehaviour
    {
        [SerializeField]
        private Image _colorImage;
        
        [Serializable]
        /// <summary>
        /// Function definition for a button click event.
        /// </summary>
        public class ColorButtonClickedEvent : UnityEvent<Color,UnityAction<Color>> {}
        [Serializable]
        public class ColorSelectCompleteEvent : UnityEvent<Color> {}

        // Event delegates triggered on click.
        [SerializeField]
        private ColorButtonClickedEvent OnClick = new ColorButtonClickedEvent();
        [SerializeField]
        private ColorSelectCompleteEvent OnChangedColor = new ColorSelectCompleteEvent();

        public void OnColorSelect()
        {
            OnClick?.Invoke(_colorImage.color,OnComplete);
        }

        void OnComplete(Color color)
        {
            if (_colorImage.color == color)
            {
                // same color
                return;
            }
            OnChangedColor?.Invoke(color);
        }
    }
}
