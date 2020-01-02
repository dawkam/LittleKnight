using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Item key;
    public GameObject door;

    private Collider _collider;
    private Notification _notification;
    private InventoryController _inventoryController;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _notification = Notification.instance;
        _inventoryController = InventoryController.instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            if (_inventoryController.ContainsItem(key) && door != null)
            {
                door.SetActive(false);
                _inventoryController.RemoveItem(key);
            }
            else
            {
                _notification.ActiveOk("You dont have key!");
            }
        }
    }


}
