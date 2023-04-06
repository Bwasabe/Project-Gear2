using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseEntityAttack, IUpdateAble, IPlayerComponentAble
{
    private PlayerAnimation _playerAnimation;
    private PlayerAnimationEventHandler _playerAnimationEventHandler;
    
    private const string ATTACK_END_CALLBACK = "AttackEndCallback";
    
    public void OnAwake(PlayerComponentController componentController)
    {
        _playerAnimation = componentController.GetPlayerComponent<PlayerAnimation>();
        _playerAnimationEventHandler = componentController.GetPlayerComponent<PlayerAnimationEventHandler>();
    }
    
    private void OnEnable() {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable() {
        UpdateManager.Instance.UnRegisterObject(this);
    }

    private void Start()
    {
        _playerAnimationEventHandler.AddEvent("AttackEndCallback", AttackEndCallback);
    }

    public void OnUpdate()
    {
        // TODO: 적을 찾아 공격
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _playerAnimation.TrySetAnimationState(PlayerAnimationState.Attack);
    }

    private void AttackEndCallback()
    {
        _playerAnimation.TrySetAnimationState(PlayerAnimationState.Idle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;
        
        if((collisionGameObject.layer & 1 << _hitLayer) <= 0) return;
        
        if(collisionGameObject.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            damaged.Damaged(_atk);
        }

    }

}
