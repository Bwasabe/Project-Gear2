using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public GameObject prefab;
    public Action onInit;
    public Action onReturn;
 
    private bool _isInit;
 
    public void Init()
    {
        if (_isInit) return;
        foreach (var component in GetComponentsInChildren<MonoBehaviour>())
        {
            switch (component)
            {
            case IPoolInitAble initAble:
                onInit += initAble.Init;

                break;
            case IPoolReturnAble returnable:
                onReturn += returnable.Return;

                break;
            }

        }
 
        _isInit = true;
    }
 
    private void Awake()
    {
        Init();
    }
}
