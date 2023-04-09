using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : BaseEntityAttack
{
    [SerializeField]
    private AudioClip _attackSound;

    protected override void Attack()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if ((collisionGameObject.layer & 1 << _hitLayer) <= 0) return;

        if (collisionGameObject.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            //Attack();
            damaged.Damaged(_atk, TextType.EnemyCritical);
        }
    }
}
