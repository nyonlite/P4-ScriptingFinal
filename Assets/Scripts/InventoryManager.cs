using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxCapacity = 10;

    [Header("Events")]
    [SerializeField] private UnityEvent onInventoryChanged;

    private List<ItemData> heldItems;

    public int Count => heldItems.Count;
    public bool IsFull => heldItems.Count >= maxCapacity;
    public List<ItemData> _heldItems => heldItems;

    private void Awake()
    {
        heldItems = new List<ItemData>();
    }

    public bool PickUp(ItemData type)
    {
        if (IsFull)
        {
            Debug.Log("Inventory is Full!");
            return false;
        }

        heldItems.Add(type);
        onInventoryChanged.Invoke();

        if(ItemDefinitions.Instance !=null
            //&& ItemDefinitions.Instance.TryGetItemData(type, out ItemData data)
            //unsure why this line wasn't working
            )
        {
            Debug.Log($"Picked up: {type}");
        }
        
        return true;
    }

    public bool UseItem(ItemData type)
    {
        if (!heldItems.Contains(type))
        {
            Debug.Log($"You don't have any {type}");
            return false;
        }
        heldItems.Remove(type);
        onInventoryChanged.Invoke();
        Debug.Log($"Used a {type}");
        return true;
    }

    public bool UseFirstItem()
    {
        if (heldItems.Count == 0)
        {
            Debug.Log($"You don't have anything");
            return false;
        }
        ItemData first = heldItems[0];
        heldItems.RemoveAt(0);
        onInventoryChanged.Invoke();
        Debug.Log($"Used a {first}");
        return true;
    }

    public bool Has(ItemData type) => heldItems.Contains(type);

    public void LogContents()
    {
        if (heldItems.Count == 0)
        {
            Debug.Log($"You don't have anything");
            return;
        }
        foreach (ItemData item in heldItems) 
        {
            Debug.Log($" - {item}");
        }
    }

    

    [ContextMenu("Test: Use First Item")]
    public void TestUseFirst() => UseFirstItem();



    [ContextMenu("Test: View Inventory")]
    public void TestViewItems() => LogContents();
}
