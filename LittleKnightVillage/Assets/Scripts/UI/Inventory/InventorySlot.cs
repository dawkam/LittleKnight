using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot :Slot
{
    public Button removeButton;


    public override void AddItem(Item newItem)
    {
        base.AddItem(newItem);
        removeButton.interactable = true;
    }


    public override void ClearSlot()
    {
        base.ClearSlot();
        removeButton.interactable = false;
    }


}
