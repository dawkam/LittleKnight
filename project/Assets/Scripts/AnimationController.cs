﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController 
{
    protected Animator _animator;

    public AnimationController()
    {  }
    public AnimationController(Animator animator)
    {
        this._animator = animator;
    }
    abstract public void Walk(float speed);
    abstract public void PerformAttack1();

    abstract public void PerformAttack2();

    abstract public bool Jump(float velocityY);

    abstract public void Die();


}
