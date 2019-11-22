
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Button removeButton;

    public GameObject description;
    public Image descriptionImage;


    private Text _descriptionText;
    Item item;


    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        SetDescription();
        if (transform.parent.parent.tag == "Inventory")
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
            if (transform.parent.parent.tag == "Inventory")
            {
                item.Use();
                description.SetActive(false);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (description != null && item != null)
        {

            description.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveDescription();
    }

    public void SetDescription()
    {
        if (description != null && _descriptionText == null)
        {
            _descriptionText = description.GetComponentInChildren<Text>();
        }
        if (_descriptionText != null && _descriptionText.text != item.GetDescription())
            _descriptionText.text = item != null ? item.GetDescription() : "";
        if (descriptionImage != null && descriptionImage.sprite != icon.sprite)
            descriptionImage.sprite = icon.sprite;
    }

    public void RemoveDescription()
    {
        if (description != null)
        {
            description.SetActive(false);
        }
    }
}
