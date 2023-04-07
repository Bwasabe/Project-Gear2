using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle = 0,
    Attack = 1,
    Move = 2,
}

[RequireComponent(typeof(Animator))]
public class AnimationController<T> : MonoBehaviour, IGetComponentAble where T : Enum
{

    public AnimationCtrl<T> AnimationCtrl{ get; private set; }
    protected void Awake() {
        Animator animator = GetComponent<Animator>();
        AnimationCtrl = new(animator);
        Debug.Log(AnimationCtrl);
    }

}
