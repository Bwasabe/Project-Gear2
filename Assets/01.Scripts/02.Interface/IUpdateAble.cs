using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateAble
{
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    public void OnUpdate();
}
