using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : AnimationController
{
    public PlayerAnimation(Animator animator) : base(animator)
    {
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void FastAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void Jump()
    {
        throw new System.NotImplementedException();
    }

    public override void StrongAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void Walk(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
}
