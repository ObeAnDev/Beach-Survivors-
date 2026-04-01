using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveEffects : MonoBehaviour
{

    List<IActiveEffect> activeEffects;

    private void Update()
    {
        ActivateEffects(Time.deltaTime);
    }
    void AddEffect(IActiveEffect effect)
    {
        activeEffects.Add(effect);
        effect.Apply(gameObject);
    }
    void RemoveEffect(IActiveEffect effect)
    {
        activeEffects.Remove(effect);
    }
    void ActivateEffects(float deltaTime)
    {
        foreach (var effect in activeEffects)
        {
            effect.Activate(deltaTime);
        }
    }
}
