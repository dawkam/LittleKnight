using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> items;
    LootModel lootModel;

    private bool isOpennig;
    private Animator _animator;

    void Start()
    {
        lootModel = LootModel.instance;
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (Input.GetButtonDown("Interact") && !isOpennig && items.Count != 0)
            {

                    isOpennig = true;
                    _animator.SetBool("IsOpennig", isOpennig);
                    // show items
                    if (lootModel.lootSize < items.Count)
                    {
                        Debug.LogError("Too many items in chest!");

                    }
                    else
                    {
                        lootModel.AddWaitingItems(items);
                    }
             
            }else if(isOpennig && items.Count == 0)
                    CloseChestAnimation();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        CloseChestAnimation();
    }

    private void CloseChestAnimation()
    {
        if (isOpennig)
        {
            isOpennig = false;
            _animator.SetBool("IsOpennig", isOpennig);
            lootModel.RemoveWaitingItems(items);
        }
    }
}
