using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AttackType
{
    Attack1,
    Attack2
}
public class KnightAttackStateMachine : StateMachineBehaviour
{
    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private AnimationCtrl<PlayerAnimationState> _animationController;
    
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        GameManager.Instance.StartCoroutine(GetPlayerAnimation());
    }

    private IEnumerator GetPlayerAnimation()
    {
        yield return Yields.WaitForEndOfFrame;
        _animationController = CharacterManager.Instance.Knight.GetPlayerComponent<CharacterAnimationController>().AnimationCtrl;
    }

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int random = (int)Define.GetRandomEnum<AttackType>();
        
        animator.SetFloat(ATTACK_HASH, random);
        
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     Debug.Log("StateUpdate");
    // }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     _characterAnimation.TrySetAnimationState(PlayerAnimationState.Idle);
    // }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
