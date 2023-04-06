using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseEntityAttack, IUpdateAble, IPlayerComponentAble
{
    // TODO: PlayerAnimationComponent 를 가져올 방법 생각해보기
    private PlayerAnimation _playerAnimation;
    
    public void OnAwake(PlayerComponentController componentController)
    {
        _playerAnimation = componentController.GetPlayerComponent<PlayerAnimation>();
        Debug.Log(_playerAnimation);
    }
    
    private void OnEnable() {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable() {
        Debug.Log("Disable");
        UpdateManager.Instance.UnRegisterObject(this);
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
        _playerAnimation.AnimationCtrl.SetAnimationStateOnce(PlayerAnimationState.Attack);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisionGameObject = other.gameObject;
        
        if((collisionGameObject.layer & 1 << _hitLayer) <= 0) return;
        
        if(collisionGameObject.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            damaged.Damaged(_atk);
        }


    }
    
}
