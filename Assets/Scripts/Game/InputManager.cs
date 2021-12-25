using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private float _clickInterval = 0.3f;
    private bool _pressed = false;
    private float _mouseDownTime;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _mouseDownTime = Time.time;
            _pressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_pressed && Time.time - _mouseDownTime <= _clickInterval)
            {
                EventBus.RaiseMouseClick(Input.mousePosition);
            }
            _pressed = false;
        }
    }
}
