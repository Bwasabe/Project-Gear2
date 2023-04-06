using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentController : MonoBehaviour
{
    private readonly Dictionary<Type, IPlayerComponentAble> _playerComponentDict = new();

    private void Awake()
    {
        IPlayerComponentAble[] playerComponentAbles = GetComponentsInChildren<IPlayerComponentAble>();

        foreach (IPlayerComponentAble componentAble in playerComponentAbles)
        {
            AddPlayerComponent(componentAble);
        }
        
        foreach (IPlayerComponentAble componentAble in playerComponentAbles)
        {
            componentAble.OnAwake(componentController: this);
        }
    }

    private void AddPlayerComponent(IPlayerComponentAble componentAble)
    {
        if(!_playerComponentDict.TryAdd(componentAble.GetType(), componentAble))
        {
            Debug.Log("IPlayerComponent overlap in Object");
        }
    }

    public T GetPlayerComponent<T>() where T : MonoBehaviour, IPlayerComponentAble
    {
        if(_playerComponentDict.TryGetValue(typeof(T), out IPlayerComponentAble value))
            return value as T;
        else
            throw new SystemException($"{nameof(T)} is none in Dictionary");
    }
}
