using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseEntityAttack, IUpdateAble
{
    // TODO: PlayerAnimationComponent 를 가져올 방법 생각해보기
    private PlayerAnimation _playerAnimation;

    private void OnEnable() {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable() {
        UpdateManager.Instance.UnRegisterObject(this);
    }

    public void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _playerAnimation.AnimationCtrl.SetAnimationStateOnce(PlayerAnimationState.Attack);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            damaged.Damaged(_atk);
        }

        // if((other.gameObject.layer & 1 << _hitLayer) >0 )
        // {
            
        // }
    }
}
