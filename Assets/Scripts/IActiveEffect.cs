using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveEffect
{
    void Apply(GameObject player);
    void Activate(float deltaTime);
}
