using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle,
    Attack,
}

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    public AnimationCtrl<PlayerAnimationState> AnimationCtrl{get; private set;}

    private void Awake() {
        Animator animator = GetComponent<Animator>();

        AnimationCtrl = new AnimationCtrl<PlayerAnimationState>(animator);
    }
}
