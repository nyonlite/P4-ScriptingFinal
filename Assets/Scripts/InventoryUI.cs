using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    private void Awake()
    {
        if (slotPrefab == null) { Debug.LogError("Missing slot prefab"); }
        if (inventoryManager == null) { Debug.LogError("Missing Inventory Manager"); }
        if (slotParent == null) { Debug.LogError("Missing slot parent"); }
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData item in inventoryManager._heldItems)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            InventorySlotUI slotUI = slot.GetComponent<InventorySlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(item);
            }
            else
            {
                Debug.LogError("Slot Prefab is missing slotUI component");
            }
                   
        }

    }
}
