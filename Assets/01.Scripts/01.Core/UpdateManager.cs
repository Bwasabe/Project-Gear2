using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoSingleton<UpdateManager>
{
    private List<IUpdateAble> _updateAbleList = new();

    private void Update()
    {
        foreach (IUpdateAble updateAble in _updateAbleList)
        {
            updateAble.OnUpdate();
        }
    }
}
