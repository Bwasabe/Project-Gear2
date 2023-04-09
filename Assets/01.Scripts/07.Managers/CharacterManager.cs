using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoSingleton<CharacterManager>
{
    [field: SerializeField]
    public EntityComponentController Knight{ get; private set; }

    private float _characterTimeScale;

    public event Action<float> OnTimeScaleChanged;

    public float CharacterTimeScale{
        get {
            return _characterTimeScale;
        }
        set {
            
            _characterTimeScale = value;
            
            OnTimeScaleChanged?.Invoke(_characterTimeScale);
        }
    }

    
    public Transform GetCharacter(Transform enemy)
    {
        if(!Knight.gameObject.activeSelf) return null;

        return Knight.transform;
        
        
    }
}
