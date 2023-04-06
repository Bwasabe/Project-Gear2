using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum CharacterState
{
    Idle = 1 << 0,
    Attack = 1 << 1,
    Move = 1 << 2,
}
public class CharacterStateController : MonoBehaviour, ICharacterComponentAble
{
    private CharacterState _currentState;

    public void AddState(CharacterState state)
    {
        _currentState |= state;
    }

    public void RemoveState(CharacterState state)
    {
        _currentState &= ~state;
    }

    public bool HasState(CharacterState state)
    {
        return _currentState.HasFlag(state);
    }
}
