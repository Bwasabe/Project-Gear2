using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterSkillBase : MonoBehaviour
{
    [field:SerializeField] private float _skillCooldown;
    [field:SerializeField] public float CooldownTimer{get; protected set;}

    [field:SerializeField] public Sprite SkillIcon{get; private set;}

    [field:SerializeField] public Color SkillBackgroundColor{get;private set;}


    public event Action<float> OnSkillTimerChanged;

    public bool IsCanUseSkill{get; protected set;}


    public abstract void ExecuteSkill();
    
}
