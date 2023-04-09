
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillController : MonoBehaviour, IGetComponentAble
{

    [SerializeField]
    private List<CharacterSkillBase> _skills;

    [SerializeField]
    private float _maxMp;

    private float _mp;

    public event Action<float> OnMpChanged;

    public void InitializeComponent(EntityComponentController componentController){
        
    }

    
}
