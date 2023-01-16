using UnityEngine;

[CreateAssetMenu(menuName = "Items", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public bool oneTimeUsage;
}
