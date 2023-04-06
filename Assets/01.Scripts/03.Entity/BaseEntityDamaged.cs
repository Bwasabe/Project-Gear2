using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityDamaged : MonoBehaviour
{
    [SerializeField]
    protected float _maxHp;

    protected float _hp;

    public event Action<float> OnDamageTaken;

    public event Action OnDied;

    protected virtual void Start()
    {
        _hp = _maxHp;
    }
    
    public virtual void Damaged(float damage)
    {
        _hp -= damage;

        OnDamageTaken?.Invoke(damage);

        if(_hp <= 0f)
        {
            OnDied?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
