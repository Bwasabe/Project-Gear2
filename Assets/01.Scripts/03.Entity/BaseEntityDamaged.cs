using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityDamaged : MonoBehaviour
{
    [SerializeField]
    protected float _maxHp;

    public float MaxHp => _maxHp;

    public float Hp{get;protected set;}

    public event Action<float, TextType> OnDamageTaken;

    public event Action OnDied;

    protected virtual void Start()
    {
        Hp = _maxHp;
    }
    
    public virtual void Damaged(float damage, TextType type)
    {
        Hp -= damage;

        OnDamageTaken?.Invoke(damage, type);

        if(Hp <= 0f)
        {
            OnDied?.Invoke();
            PoolManager.Destroy(gameObject);
        }
    }
}
