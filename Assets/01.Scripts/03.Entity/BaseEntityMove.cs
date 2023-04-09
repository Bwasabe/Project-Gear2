using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityMove : MonoBehaviour
{
    [field:SerializeField]
    public float Speed{get;set;}

    protected abstract void Move();
}
