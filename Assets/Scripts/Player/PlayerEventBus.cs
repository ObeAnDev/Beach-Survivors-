using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventBus 
{
    public static Action<float> OnHealthChanged;
    public static Action<float> OnExpChanged;
    public static Action<int> OnLevelChanged;
}
