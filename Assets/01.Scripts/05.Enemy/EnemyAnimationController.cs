using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimationState
{
    Idle = 0,
    Attack = 1,
    Move = 2,
}
public class EnemyAnimationController : AnimationController<EnemyAnimationState>
{
    
}
