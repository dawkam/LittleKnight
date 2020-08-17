using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAnimation : MonoBehaviour
{
    protected Animator _anim;
    public abstract void IdleAnimation();
    public abstract void WalkAnimation();
    public abstract bool AttackAnimation();
    public abstract void DieAnimation();

    public abstract bool DieAnimationEnd();

    public bool IsIdleAniamtion()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }
}
