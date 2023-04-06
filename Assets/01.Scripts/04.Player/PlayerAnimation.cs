using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle = 0,
    Attack = 1,
}

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour, IPlayerComponentAble
{
    public AnimationCtrl<PlayerAnimationState> AnimationCtrl{get; private set;}

    private void Awake() {
        Animator animator = GetComponent<Animator>();

        AnimationCtrl = new AnimationCtrl<PlayerAnimationState>(animator);
    }
    public void OnAwake(PlayerComponentController componentController)
    {
        
    }
}
