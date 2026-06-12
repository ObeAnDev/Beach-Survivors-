using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string cardText;
    public Sprite cardImage;
}
