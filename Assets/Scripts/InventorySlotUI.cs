using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemText;

    private void Awake()
    {
        if (icon == null) { Debug.LogError("Missing icon"); }
        if (itemText == null) { Debug.LogError("Missing item text"); }
    }

    public void SetData(ItemData data)
    {
        if (data == null)
        {
            icon.sprite = null;
            itemText.text = "";
            return;
        }

        icon.sprite = data.Icon();
        itemText.text = data.ItemName();
    }
}
