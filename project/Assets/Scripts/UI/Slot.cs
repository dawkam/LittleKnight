using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // każdy z typów slotów jest widokiem dla konkretnej funkcji np.InventoryModel , InventoryController, InventorySlot(jest ich wiele) 
    public Image icon;
    public GameObject description;
    public Image descriptionImage;

    protected Text[] _descriptionText;
    protected Item _item;

    public virtual void AddItem(Item newItem)
    {
        _item = newItem;

        icon.sprite = _item.icon;
        icon.enabled = true;
        SetDescription();

    }
    public virtual void ClearSlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (description != null && _item != null)
        {
            description.SetActive(true);
        }
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        RemoveDescription();
    }

    public virtual void SetDescription()
    {
        if (description != null && _descriptionText == null)
        {
            _descriptionText = description.GetComponentsInChildren<Text>();
        }
        if (_descriptionText != null && _descriptionText[0].text != _item.GetName())
            _descriptionText[0].text = _item != null ? _item.GetName() : "No Name";
        if (_descriptionText != null && _descriptionText[1].text != _item.GetDescription())
            _descriptionText[1].text = _item != null ? _item.GetDescription() : "No description";
        if (descriptionImage != null && descriptionImage.sprite != icon.sprite)
            descriptionImage.sprite = icon.sprite;
    }
    public virtual void RemoveDescription()
    {
        if (description != null)
        {
            description.SetActive(false);
        }
    }

    public virtual Item GetItem()
    {
        return _item;
    }

    public virtual void UseItem()
    {
        _item.Use();
    }
}
