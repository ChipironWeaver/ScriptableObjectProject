using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public string itemDescription;
}
