using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static event Action<Vector3> MouseClick;
    public static void RaiseMouseClick(Vector3 arg)
    {
        MouseClick?.Invoke(arg);
    }
}
