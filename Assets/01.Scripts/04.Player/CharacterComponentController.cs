using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponentController : MonoBehaviour
{
    private readonly Dictionary<Type, ICharacterComponentAble> _playerComponentDict = new();

    private void Awake()
    {
        Debug.Log("Awake");
        ICharacterComponentAble[] playerComponentAbles = GetComponentsInChildren<ICharacterComponentAble>();

        foreach (ICharacterComponentAble componentAble in playerComponentAbles)
        {
            AddPlayerComponent(componentAble);
        }
        
        foreach (ICharacterComponentAble componentAble in playerComponentAbles)
        {
            componentAble.InitializePlayerComponent(componentController: this);
        }
    }

    private void AddPlayerComponent(ICharacterComponentAble componentAble)
    {
        if(!_playerComponentDict.TryAdd(componentAble.GetType(), componentAble))
        {
            Debug.Log("IPlayerComponent overlap in Object");
        }
    }

    public T GetPlayerComponent<T>() where T : MonoBehaviour, ICharacterComponentAble
    {
        if(_playerComponentDict.TryGetValue(typeof(T), out ICharacterComponentAble value))
            return value as T;
        else
            throw new SystemException($"{nameof(T)} is none in Dictionary");
    }
}
