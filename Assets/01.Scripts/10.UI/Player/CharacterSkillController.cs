
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillController : MonoBehaviour, IGetComponentAble
{

    [SerializeField]
    private float _maxMp;

    private float _mp;

    public event Action<float> OnMpChanged;

    private void Awake() {
       _mp = _maxMp; 
    }

    // public void InitializeComponent(EntityComponentController componentController){
        
    // }

    public void UseMp(float mp)
    {
        _mp -= mp;
        OnMpChanged?.Invoke(_mp);
    }

    public bool IsCanMpEnough(float mp)
    {
        return _mp >= mp;
    }
}
