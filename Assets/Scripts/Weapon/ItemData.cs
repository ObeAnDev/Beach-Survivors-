using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttachment", menuName = "BeachSurvivors/Attachment Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    public float damageMultiplier = 1.2f;
    public float coolDownReduction = 0f;
    public float speedBoost = 0f;
}
