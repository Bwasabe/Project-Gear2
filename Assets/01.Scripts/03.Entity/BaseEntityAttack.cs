using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityAttack : MonoBehaviour
{
    [SerializeField]
    protected float _atk;

    [SerializeField]
    protected LayerMask _hitLayer;

    protected abstract void Attack();
}
