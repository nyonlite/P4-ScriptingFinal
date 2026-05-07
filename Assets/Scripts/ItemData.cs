using UnityEngine;

[CreateAssetMenu(fileName = "New Item" , menuName = "Inventory/Item")]

public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private float value;
    [SerializeField] private Sprite icon;


    public ItemType Type() { return type; }
    public string ItemName() { return itemName; } 
    public float Value() { return value; }
    public Sprite Icon() { return icon; }

         
}


