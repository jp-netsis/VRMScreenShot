using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace jp.netsis.VRMScreenShot.UI
{
    [RequireComponent(typeof(Camera))]
    public class CameraMove : MonoBehaviour
    {
        private const float KEYBOARD_MOVE_DELTA = 5f;

        // Rollback Initialized Params in Editor.
        private Vector3 _initCameraPosision;
        private Vector3 _initCameraRotation;
        private float _initCameraZoom;
        private Vector3 _initCameraLookAt;
        private float _initCameraFov;
        private bool _initIsOrtho;
        private float _initOrthoSize;
        // 
        private bool _isPressedRightButton;
        private Vector2 _mouseRightStartPos;
        private Vector3 _camRightStartRot;

        private bool _isPressedMiddleButton;
        private Vector2 _mouseMiddleStartPos;
        private Vector3 _camMiddleStartPos;
        
        //
        private float _mouseScrollY;

        private Vector2 _touchStartPos;
        private Vector3 _camTouchStartRot;

        private float _pinchBeginDistance;

        [SerializeField]
        private CameraScriptableObject _cameraScriptableObject;

        [SerializeField]
        private Transform _transformRoot;

        private void OnEnable()
        {
            _initCameraPosision = _cameraScriptableObject.Position;
            _initCameraRotation = _cameraScriptableObject.Rotation;
            _initCameraZoom = _cameraScriptableObject.Zoom;
            _initCameraLookAt = _cameraScriptableObject.LookAt;
            _initCameraFov = _cameraScriptableObject.FOV;
            _initIsOrtho = _cameraScriptableObject.IsOrtho;
            _initOrthoSize = _cameraScriptableObject.OrthoSize;

            if (Application.isEditor)
            {
                TouchSimulation.Enable();
            }
            EnhancedTouchSupport.Enable();
        }

        void Update()
        {
            UpdateKeyboard();
            UpdateMouse();
            UpdateTouch();
        }

        private void OnDisable()
        {
            _cameraScriptableObject.Position = _initCameraPosision;
            _cameraScriptableObject.Rotation = _initCameraRotation;
            _cameraScriptableObject.Zoom = _initCameraZoom;
            _cameraScriptableObject.LookAt = _initCameraLookAt;
            _cameraScriptableObject.FOV = _initCameraFov;
            _cameraScriptableObject.IsOrtho = _initIsOrtho;
            _cameraScriptableObject.OrthoSize = _initOrthoSize;
            
            if (Application.isEditor)
            {
                TouchSimulation.Disable();
            }
            EnhancedTouchSupport.Disable();
        }

        void UpdateKeyboard()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                bool isKeyInput = false;
                var camPos = _cameraScriptableObject.Position;
                if (keyboard.wKey.isPressed) 
                {
                    camPos += _transformRoot.forward * Time.deltaTime * KEYBOARD_MOVE_DELTA;
                    isKeyInput = true;
                }
                if (keyboard.sKey.isPressed)
                {
                    camPos -= _transformRoot.forward * Time.deltaTime * KEYBOARD_MOVE_DELTA;
                    isKeyInput = true;
                }

                if (keyboard.aKey.isPressed)
                {
                    camPos -= _transformRoot.right   * Time.deltaTime * KEYBOARD_MOVE_DELTA;
                    isKeyInput = true;
                }

                if (keyboard.dKey.isPressed)
                {
                    camPos += _transformRoot.right   * Time.deltaTime * KEYBOARD_MOVE_DELTA;
                    isKeyInput = true;
                }

                if (keyboard.qKey.isPressed)
                {
                    camPos -= _transformRoot.up      * Time.deltaTime * KEYBOARD_MOVE_DELTA;
                    isKeyInput = true;
                }

                if (keyboard.eKey.isPressed)
                {
                    camPos += _transformRoot.up      * Time.deltaTime * KEYBOARD_MOVE_DELTA;
                    isKeyInput = true;
                }

                if (isKeyInput)
                {
                    _cameraScriptableObject.Position = camPos;
                }
            }
        }

        void UpdateMouse()
        {
            UpdateMouseRightButton();
            UpdateMouseMiddleButton();
            UpdateMouseScroll();
        }

        void UpdateMouseRightButton()
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }
            // Check if the mouse was clicked over a UI element
            if (!_isPressedRightButton && IsPointerOverGameObject())
            {
                return;
            }

            Vector2 mousePos = mouse.position.ReadValue();

            if (mouse.rightButton.wasPressedThisFrame)
            {
                _isPressedRightButton = true;
                _mouseRightStartPos = mousePos;
                _camRightStartRot = _cameraScriptableObject.Rotation;
            }

            if (mouse.rightButton.isPressed) 
            {
                if (_isPressedRightButton)
                {
                    float x = (_mouseRightStartPos.x - mousePos.x) / Screen.width;
                    float y = (_mouseRightStartPos.y - mousePos.y) / Screen.height;
                    _cameraScriptableObject.Rotation = new Vector3( _camRightStartRot.x - y * 90f, _camRightStartRot.y + x * 90f, 0);
                }
            }
            else
            {
                _isPressedRightButton = false;
            }
        }
        
        void UpdateMouseMiddleButton()
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }
            // Check if the mouse was clicked over a UI element
            if (!_isPressedMiddleButton && IsPointerOverGameObject())
            {
                return;
            }

            Vector2 mousePos = mouse.position.ReadValue();

            if (mouse.middleButton.wasPressedThisFrame)
            {
                _isPressedMiddleButton = true;
                _mouseMiddleStartPos = mousePos;
                _camMiddleStartPos = _cameraScriptableObject.Position;
            }

            if (mouse.middleButton.isPressed) 
            {
                if (_isPressedMiddleButton)
                {
                    float x = (_mouseMiddleStartPos.x - mousePos.x) / Screen.width;
                    float y = (_mouseMiddleStartPos.y - mousePos.y) / Screen.height;
                    Vector3 moveDelta = Quaternion.Euler(_cameraScriptableObject.Rotation) * new Vector3(x,y,0f);
                    _cameraScriptableObject.Position = _camMiddleStartPos + moveDelta;
                }
            }
            else
            {
                _isPressedMiddleButton = false;
            }
        }

        void UpdateMouseScroll()
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }
            // Check if the mouse was scroll over a UI element
            if (IsPointerOverGameObject())
            {
                return;
            }
            var mouseScroll = Mouse.current.scroll.ReadValue();
            if (mouseScroll != Vector2.zero)
            {
                _mouseScrollY = _cameraScriptableObject.Zoom;
                _mouseScrollY += (mouseScroll.y * 0.0001f) * -1;
                _mouseScrollY = Mathf.Clamp(_mouseScrollY, -3f, 0.7f);
                _cameraScriptableObject.Zoom = _mouseScrollY;
            }
        }

        void UpdateTouch()
        {
            UpdateTouchSingle();
            UpdateTouchMulti();
        }

        void UpdateTouchSingle()
        {
            if (Touch.activeFingers.Count == 0 || Touch.activeFingers.Count >= 2)
            {
                return;
            }
            var touch = Touch.activeFingers[0].currentTouch;
            // Check if the mouse was clicked over a UI element
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began && IsPointerOverGameObject())
            {
                return;
            }

            Vector2 touchPos = touch.screenPosition;

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                _touchStartPos = touch.startScreenPosition;
                _camTouchStartRot = _cameraScriptableObject.Rotation;
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved) 
            {
                float x = (_touchStartPos.x - touchPos.x) / Screen.width;
                float y = (_touchStartPos.y - touchPos.y) / Screen.height;
                _cameraScriptableObject.Rotation = new Vector3( _camTouchStartRot.x - y * 90f, _camTouchStartRot.y + x * 90f, 0);
            }
        }

        void UpdateTouchMulti()
        {
            if (Touch.activeFingers.Count == 2)
            {
                // PinchInOut
                var touch0 = Touch.activeFingers[0].currentTouch;
                var touch1 = Touch.activeFingers[1].currentTouch;

                if (touch1.phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    _pinchBeginDistance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);
                    _mouseScrollY = _cameraScriptableObject.Zoom;
                }

                if (!(touch0.phase == UnityEngine.InputSystem.TouchPhase.Moved || touch1.phase == UnityEngine.InputSystem.TouchPhase.Moved))
                {
                    return;
                }
                float distance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);
                OnPinch(distance - _pinchBeginDistance);
                _pinchBeginDistance = distance;

            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta">0>delta:Pinch In(ZoomOut) 0<delta:Pinch Out(ZoomIn)</param>
        void OnPinch(float delta)
        {
            _mouseScrollY += delta * 0.01f;
            _mouseScrollY = Mathf.Clamp(_mouseScrollY, -3f, 0.7f);
            _cameraScriptableObject.Zoom = _mouseScrollY;
        }

        bool IsPointerOverGameObject()
        {
            //check mouse
            if(EventSystem.current.IsPointerOverGameObject())
                return true;
             
            //check touch
            if(0 < Touch.fingers.Count)
            {
                if (EventSystem.current.IsPointerOverGameObject(Touch.fingers[0].index))
                {
                    return true;
                }
            }
            return false;
        }
    }
}