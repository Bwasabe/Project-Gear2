using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour, IGetComponentAble
{

    private readonly Dictionary<string, Action> _playerAnimationEventDict = new();

    protected void OnAnimationExecute(string actionName)
    {
        if(_playerAnimationEventDict.TryGetValue(actionName, out Action action))
            _playerAnimationEventDict[actionName]?.Invoke();
        else
            throw new SystemException("ActionName is wrong please check the parameter");
    }

    public void AddEvent(string actionName, Action action)
    {
        if(!_playerAnimationEventDict.TryAdd(actionName, action))
            throw new System.Exception("ActionName is already exist in Dictionary or Action is Wrong");
    }
}
