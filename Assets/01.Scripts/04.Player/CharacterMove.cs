using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMove : BaseEntityMove, IUpdateAble, ICharacterComponentAble
{
    private Rigidbody2D _rb;

    private CharacterAttack _characterAttack;
    private CharacterAnimation _characterAnimation;
    private CharacterStateController _stateController;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void InitializePlayerComponent(CharacterComponentController componentController)
    {
        _characterAnimation = componentController.GetPlayerComponent<CharacterAnimation>();
        _stateController = componentController.GetPlayerComponent<CharacterStateController>();
        _characterAttack = componentController.GetPlayerComponent<CharacterAttack>();
    }

    private void OnEnable() {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable() {
        UpdateManager.Instance.UnRegisterObject(this);
    }
    
    public void OnUpdate()
    {
        Move();
        FlipCharacter();
        SetAnimationByVelocity();
    }

    protected override void Move()
    {
        if(_characterAttack.Target == null || _stateController.HasState(CharacterState.Attack))
        {
            _rb.velocity = Vector2.zero;

            return;
        }
        
        Vector2 dir = (_characterAttack.Target.position - transform.position).normalized;
        _rb.velocity = dir * _speed;
    }
    
    private void SetAnimationByVelocity()
    {
        if(_stateController.HasState(CharacterState.Attack)) return;

        if(_rb.velocity == Vector2.zero)
        {
            _characterAnimation.TrySetAnimationState(PlayerAnimationState.Idle);
            
            _stateController.AddState(CharacterState.Idle);
            _stateController.RemoveState(CharacterState.Move);
        }
        else
        {
            _stateController.AddState(CharacterState.Move);
            _stateController.RemoveState(CharacterState.Idle);
            
            _characterAnimation.TrySetAnimationState(PlayerAnimationState.Move);
        }
    }

    private void FlipCharacter()
    {
        if(_stateController.HasState(CharacterState.Attack)) return;
        
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);
        if(_rb.velocity.x < 0)
            scale.x = scaleX * -1f;
        else
            scale.x = scaleX;

        transform.localScale = scale;
    }

    private void SetInput(ref Vector2 input)
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    
    
}
