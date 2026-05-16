using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "Drop", menuName = "dropSystem")]
public class DropRateSO : ScriptableObject
{
    public float dropRate;
}
