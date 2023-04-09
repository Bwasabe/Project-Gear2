using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterSkillBase : MonoBehaviour
{
    [field:SerializeField] public float SkillCooldown{get; private set;}

    [field:SerializeField] public float CooldownTimer{get; private set;}

    [field:SerializeField] public Sprite SkillIcon{get; private set;}

    public abstract void ExecuteSkill();
    
}
