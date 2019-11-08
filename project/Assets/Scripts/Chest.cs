using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isOpennig;
    private Animator _animator;

    private PlayerAnimation _chestAnimation;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
        private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            KeyCode interactKey = other.gameObject.GetComponent<PlayerModel>().interactKey;

            if (Input.GetKeyDown(interactKey))
            {
                isOpennig = true;
                _animator.SetBool("IsOpennig", isOpennig);
                // show items
                // take items etc
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpennig)
        {
            isOpennig = false;
            _animator.SetBool("IsOpennig", isOpennig);
        }
    }
}
