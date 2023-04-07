﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityComponentController : MonoBehaviour
{
    private readonly Dictionary<Type, IGetComponentAble> _playerComponentDict = new();

    private IGetComponentAble[] _componentAbles;
    private void Awake()
    {
        _componentAbles = GetComponentsInChildren<IGetComponentAble>();

        foreach (IGetComponentAble componentAble in _componentAbles)
        {
            AddPlayerComponent(componentAble);
        }
        foreach (IGetComponentAble componentAble in _componentAbles)
        {
            componentAble.InitializeComponent(componentController: this);
        }
    }

    // private IEnumerator Start()
    // {
    //     yield return Yields.WaitForEndOfFrame;
    //     
    //     
    // }

    private void AddPlayerComponent(IGetComponentAble componentAble)
    {
        if(!_playerComponentDict.TryAdd(componentAble.GetType(), componentAble))
        {
            Debug.Log("IPlayerComponent overlap in Object");
        }
    }

    public T GetEntityComponent<T>() where T : MonoBehaviour, IGetComponentAble
    {
        if(_playerComponentDict.TryGetValue(typeof(T), out IGetComponentAble value))
            return value as T;
        else
            throw new SystemException($"{typeof(T).Name} is none in Dictionary");
    }
}
