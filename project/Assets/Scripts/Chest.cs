using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> items;
    LootSystem lootSystem;

    private bool isOpennig;
    private Animator _animator;

    private PlayerAnimation _chestAnimation;
    void Start()
    {
        lootSystem = LootSystem.instance;
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (Input.GetButtonDown("Interact") && !isOpennig)
            {
                if (items.Count != 0)
                {
                    isOpennig = true;
                    _animator.SetBool("IsOpennig", isOpennig);
                    // show items
                    if (lootSystem.lootSize < items.Count)
                    {
                        Debug.LogError("Too many items in chest!");

                    }
                    else
                    {
                        lootSystem.AddWaitingItems(items);
                    }
                    // take items etc
                }
                else
                {
                    //alert o braku itemów
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpennig)
        {
            isOpennig = false;
            _animator.SetBool("IsOpennig", isOpennig);
            lootSystem.RemoveWaitingItems(items);
        }
    }
}
