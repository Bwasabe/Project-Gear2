using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateAble
{
    protected void OnEnable();

    protected void OnDisable();

    public void OnUpdate();
}
