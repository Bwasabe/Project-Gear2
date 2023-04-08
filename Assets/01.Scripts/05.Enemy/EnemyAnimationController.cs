using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimationState
{
    None = -1,
    Idle = 0,
    Attack = 1,
    Move = 2,
}
public class EnemyAnimationController : AnimationController<EnemyAnimationState>, IPoolReturnAble
{

    public void Return()
    {
        AnimationCtrl.SetAnimationState(EnemyAnimationState.None);
    }
}
