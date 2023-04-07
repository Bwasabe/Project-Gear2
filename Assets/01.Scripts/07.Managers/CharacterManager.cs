using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoSingleton<CharacterManager>
{
    [field: SerializeField]
    public EntityComponentController Knight{ get; private set; }

    private List<Transform> _characters = new();

    private void Awake()
    {
        _characters.Add(Knight.transform);
    }
    

    public Transform GetClosestCharacter(Transform enemy)
    {
        if(_characters.Count <= 0) return null;

        Transform closestCharacter = null;

        float minSqrMagnitude = float.MaxValue;

        foreach (Transform character in _characters)
        {
            if(!character.gameObject.activeSelf) continue;
            
            float sqrMagnitude = (character.position - enemy.position).sqrMagnitude;
            
            if(sqrMagnitude < minSqrMagnitude)
            {
                minSqrMagnitude = sqrMagnitude;
                closestCharacter = character;
            }
        }

        return closestCharacter;
    }
}
