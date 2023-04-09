using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMove : BaseEntityMove, IUpdateAble, IGetComponentAble
{
    [SerializeField]
    private FloatingJoystick _joystick;
    
    private Rigidbody2D _rb;

    private CharacterAttack _characterAttack;
    private AnimationCtrl<CharacterAnimationState> _animationController;
    private CharacterStateController _characterStateController;

    private CharacterAnimationController _characterAnimationController;
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeComponent(EntityComponentController componentController)
    {
        _characterAnimationController = componentController.GetEntityComponent<CharacterAnimationController>();
        
        _characterStateController = componentController.GetEntityComponent<CharacterStateController>();
        _characterAttack = componentController.GetEntityComponent<CharacterAttack>();
    }

    private void Start()
    {
        _animationController = _characterAnimationController.AnimationCtrl;
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
        if(_joystick.Active)
        {
            _rb.velocity = _joystick.Direction * _speed;
        }
        else if(_characterAttack.Target == null || _characterStateController.HasState(CharacterState.Attack))
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 dir = (_characterAttack.Target.position - transform.position).normalized;
            _rb.velocity = dir * _speed;
        }
        
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



}
