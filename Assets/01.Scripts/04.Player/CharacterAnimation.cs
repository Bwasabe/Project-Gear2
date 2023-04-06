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
public class CharacterAnimation : MonoBehaviour, ICharacterComponentAble
{
    private readonly int STATE_HASH = Animator.StringToHash("State");
    

    private PlayerAnimationState _currentAnimationState;

    private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();

    }

    public void SetAnimationState(PlayerAnimationState animationState)
    {
        _currentAnimationState = animationState;
        _animator.SetInteger(STATE_HASH, (int)animationState);
    }

    public void TrySetAnimationState(PlayerAnimationState animationState)
    {
        if (_currentAnimationState != animationState)
            SetAnimationState(animationState);
    }
}
