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
public class CharacterAnimationController : MonoBehaviour, IGetComponentAble
{
    private readonly int STATE_HASH = Animator.StringToHash("State");
    

    public AnimationCtrl<PlayerAnimationState> AnimationCtrl{ get; private set; }
    private void Awake() {
        Animator animator = GetComponent<Animator>();
        AnimationCtrl = new(animator);
        Debug.Log(AnimationCtrl);
    }

}
