using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        }

        void Update()
        {
            UpdateKeyboard();
            UpdateMouse();
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
                _mouseScrollY += (mouseScroll.y * 0.0001f) * -1;
                _mouseScrollY = Mathf.Clamp(_mouseScrollY, -3f, 0.7f);
                _cameraScriptableObject.Zoom = _mouseScrollY;
            }
        }

        bool IsPointerOverGameObject()
        {
            //check mouse
            if(EventSystem.current.IsPointerOverGameObject())
                return true;
             
            //check touch
            if(Touchscreen.current != null && 0 < Touchscreen.current.touches.Count)
            {
                if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
                {
                    return true;
                }
            }
             
            return false;
        }
    }
}