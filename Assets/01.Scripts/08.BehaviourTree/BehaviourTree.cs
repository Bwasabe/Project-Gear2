using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour, IUpdateAble
{
    public abstract BT_Variable Variable{ get; set; }

    protected BT_Node _root;

    public bool IsStop{ get; set; } = false;

    protected virtual void Start()
    {
        _root = SetupTree();
    }

    protected virtual void OnEnable()
    {
        UpdateManager.Instance.RegisterObject(this);
    }

    protected void OnDisable()
    {
        UpdateManager.Instance?.UnRegisterObject(this);
    }

    protected abstract BT_Node SetupTree();
    public void OnUpdate()
    {
        if(!IsStop)
        {
            _root?.Execute();
        }
    }
}