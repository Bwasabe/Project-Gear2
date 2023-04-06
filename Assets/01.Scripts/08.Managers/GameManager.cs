using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public CharacterComponentController Character{
        get {
            
            _character ??= FindObjectOfType<CharacterComponentController>();
            return _character;
        }
    }

    private CharacterComponentController _character;
}
