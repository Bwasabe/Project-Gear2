using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityDamaged : MonoBehaviour
{
    [SerializeField]
    protected float _maxHp;

    private float _hp;
    
    public virtual void Damaged(float damage)
    {
        
    }
}
