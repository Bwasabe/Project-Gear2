using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMove : BaseEntityMove, IUpdateAble, IGetComponentAble
{
    private Rigidbody2D _rb;

    private CharacterAttack _characterAttack;
    private AnimationCtrl<CharacterAnimationState> _animationController;
    private CharacterStateController _characterStateController;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeComponent(EntityComponentController componentController)
    {
        _animationController = componentController.GetPlayerComponent<CharacterAnimationController>().AnimationCtrl;
        
        _characterStateController = componentController.GetPlayerComponent<CharacterStateController>();
        _characterAttack = componentController.GetPlayerComponent<CharacterAttack>();
    }

    private void OnEnable() {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable() {
        UpdateManager.Instance?.UnRegisterObject(this);
    }
    
    public void OnUpdate()
    {
        Move();
        FlipCharacter();
        SetAnimationByVelocity();
    }

    protected override void Move()
    {
        if(_characterAttack.Target == null || _characterStateController.HasState(CharacterState.Attack))
        {
            _rb.velocity = Vector2.zero;

            return;
        }
        
        Vector2 dir = (_characterAttack.Target.position - transform.position).normalized;
        _rb.velocity = dir * _speed;
    }
    
    private void SetAnimationByVelocity()
    {
        if(_characterStateController.HasState(CharacterState.Attack)) return;

        if(_rb.velocity == Vector2.zero)
        {
            _animationController.TrySetAnimationState(CharacterAnimationState.Idle);
            
            _characterStateController.AddState(CharacterState.Idle);
            _characterStateController.RemoveState(CharacterState.Move);
        }
        else
        {
            _characterStateController.AddState(CharacterState.Move);
            _characterStateController.RemoveState(CharacterState.Idle);
            
            _animationController.TrySetAnimationState(CharacterAnimationState.Move);
        }
    }

    private void FlipCharacter()
    {
        if(_characterStateController.HasState(CharacterState.Attack)) return;
        
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);
        if(_rb.velocity.x < 0)
            scale.x = scaleX * -1f;
        else
            scale.x = scaleX;

        transform.localScale = scale;
    }
    //
    // private void SetInput(ref Vector2 input)
    // {
    //     input.x = Input.GetAxisRaw("Horizontal");
    //     input.y = Input.GetAxisRaw("Vertical");
    // }

    
    
}
