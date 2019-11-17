
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    private bool isInventory;

    Item item;

    public void Awake()
    {
        isInventory = transform.parent.parent.tag == "Inventory";
    }


    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        if (isInventory)
            removeButton.interactable = true;
    }


    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        //alert
        LootSystem.instance.RemoveInventoryItems(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (isInventory)
            {
                item.Use();
            }
            else if (LootSystem.instance.AddInventoryItem(item)) //próba dodania do inventory
            {
                LootSystem.instance.RemoveWaitingItem(item); //usuwanie z okna lootu

            }
            else
            {

                //alert
            }
        }
    }

}
