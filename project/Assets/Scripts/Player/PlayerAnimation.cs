using UnityEngine;

public class PlayerAnimation : AnimationController
{
    public PlayerAnimation(Animator animator) : base(animator)
    {
    }

    public override void Die()
    {
        _animator.SetTrigger("Die");
    }


    public override bool Jump(float velocityY) // return if hump is started
    {
        if (velocityY > 0)
        {

            _animator.SetFloat("VelocityY", velocityY);
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                    && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.71f)
            {
                _animator.enabled = false;
                return true;
            }
        }
            else if(_animator.enabled == false)
                _animator.enabled = true;
        return false;
    }



    public override void Walk(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }

    public override void PerformAttack1()
    {
        _animator.SetTrigger("Attack1");
    }
    public override void PerformAttack2()
    {
        _animator.SetTrigger("Attack2");
    }

    public bool IsAttacking()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2");
    }
}
