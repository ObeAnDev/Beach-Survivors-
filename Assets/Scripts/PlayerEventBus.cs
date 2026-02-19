using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventBus 
{
    public static Action<float> OnHealthChanged;
}
