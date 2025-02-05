using UnityEngine;

public class NavigationButton : MonoBehaviour
{
    [SerializeField] private TypeInInventory _type;
    [SerializeField] private Inventory inventory;
    public TypeInInventory Type { get { return _type; } }
    public void Navigation()
    {
        for (int i = 0; i < inventory.InventoryPanel.childCount; i++)
        {        
            inventory.Slots[i].gameObject.SetActive(inventory.Slots[i].Item != null && inventory.Slots[i].Item.TypeInInvetory == _type);
        }
    }
    public void NavigationReset()
    {
        for (int i = 0; i < inventory.InventoryPanel.childCount; i++)
            inventory.Slots[i].gameObject.SetActive(true);
    }
}
