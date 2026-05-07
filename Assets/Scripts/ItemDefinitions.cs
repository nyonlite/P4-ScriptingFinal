using System.Collections.Generic;
using UnityEngine;

public class ItemDefinitions : MonoBehaviour
{
    [SerializeField] private ItemData coinData;
    [SerializeField] private ItemData potionData;
    [SerializeField] private ItemData bootsData;

    private Dictionary<ItemType, ItemData> m_items;
    public static ItemDefinitions Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.Log("Multiple instances");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        BuildDatabase();
    }

    private void BuildDatabase()
    {
        m_items = new Dictionary<ItemType, ItemData>
        {
            { ItemType.Coin,  coinData},
            { ItemType.Potion, potionData},
            { ItemType.Boots, bootsData }
        };
    }
}
