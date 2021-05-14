using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GhostAnimation : EnemyAnimation
{
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public override void IdleAnimation()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            _anim.SetTrigger("Idle");

        //if (!_anim.GetBool("isIdle"))
        //{
        //    _anim.CrossFade("Idle", 0.1f);
        //    _anim.SetBool("isIdle", true);
        //    _anim.SetBool("isWalk", false);
        //    _anim.SetBool("isAttack", false);
        //    _anim.SetBool("isDie", false);
        //}
    }

    public override void WalkAnimation()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            _anim.SetTrigger("Walk");

        //if (!_anim.GetBool("isWalk"))
        //{
        //    _anim.CrossFade("Walk", 0.1f);
        //    _anim.SetBool("isWalk", true);
        //    _anim.SetBool("isDie", false);
        //    _anim.SetBool("isIdle", false);
        //    _anim.SetBool("isAttack", false);
        //}
    }

    public override bool AttackAnimation()
    {
        if(!_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            _anim.SetTrigger("Attack");
        //if (!_anim.GetBool("isAttack"))
        //{
        //    _anim.CrossFade("Attack", 0.1f);
        //    _anim.SetBool("isWalk", false);
        //    _anim.SetBool("isDie", false);
        //    _anim.SetBool("isAttack", true);
        //    _anim.SetBool("isIdle", false);
        //}

        //Attack the target
        return (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.33f);
        //firePoint.Fire(target.position, speed, lifeSpawn, damage, damageType);
    }

    public override void DieAnimation()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            _anim.SetTrigger("Die");
           //// _anim.CrossFade("Die", 0.1f);
           // _anim.SetBool("isWalk", false);
           // _anim.SetBool("isDie", true);
           // _anim.SetBool("isAttack", false);
           // _anim.SetBool("isIdle", false);

    }

    public override bool DieAnimationEnd()
    {

        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Die") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }


}
