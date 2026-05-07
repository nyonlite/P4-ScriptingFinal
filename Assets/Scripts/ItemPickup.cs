using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData type;
    [SerializeField] private GameManager manager;
    
   
    void OnStart()
    {
       
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                bool pickedUp = inventory.PickUp(type);
                if (pickedUp)
                {
                    if (type.Type() == ItemType.Coin)
                    {
                        Debug.Log(type.Type());
                        manager.AddGold(1);
                    }
                    
                        Destroy(gameObject);
                    
                }
            }
        }
    }


}

