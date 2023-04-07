using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    public abstract BT_Data Data{ get; set; }

    protected BT_Node _root;

    public bool IsStop{ get; set; } = false;

    protected virtual void Start()
    {
        _root = SetupTree();
    }

    protected virtual void Update()
    {
        if(!IsStop)
        {
            _root?.Execute();
        }
    }

    protected abstract BT_Node SetupTree();
}