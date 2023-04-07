using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAnimationState
{
    Idle = 0,
    Attack = 1,
    Move = 2,
}


public class CharacterAnimationController : AnimationController<CharacterAnimationState>
{
    
}
