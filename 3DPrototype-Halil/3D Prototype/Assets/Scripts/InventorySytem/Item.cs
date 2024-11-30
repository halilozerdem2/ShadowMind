using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "NewItem")]
public class Item: ScriptableObject
{
    public string Name;
    public string Type;
    public Sprite Icon;

}


