using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityMove : MonoBehaviour
{
    [SerializeField]
    protected float _speed;

    protected abstract void Move();
}
