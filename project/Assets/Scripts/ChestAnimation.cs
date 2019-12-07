using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void OpenAnimation(bool isOpennig)
    {
        _animator.SetBool("Open", isOpennig);
    }

    public void CloseAnimation()
    {
        _animator.SetTrigger("Close");
    }

    public bool EndCloseAnimation()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("chest_close") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }
}
