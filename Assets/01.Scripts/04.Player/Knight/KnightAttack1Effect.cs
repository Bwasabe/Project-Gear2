using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KnightAttack1Effect : MonoBehaviour, IGetComponentAble
{
    [Header("Attack1")]
    [field: SerializeField]
    private Vector3 _rotation = new Vector3(-120f, 0f, 0f);
    [SerializeField]
    private Vector3 _offset = new Vector3(0.55f, -0.3f, 0f);
    [SerializeField]
    private Vector3 _scale = new Vector3(-0.4f, 0.2f, -0.3f);

    
    [SerializeField]
    private VisualEffect _visualEffect;

    
    private AnimationEventHandler _animationEventHandler;
    
    
    private const string ATTACK1_EFFECT = "Attack1Effect";

    private const string ATTACK_END_CALLBACK = "AttackEndCallback";
    
    
    private void Start()
    {
        _animationEventHandler.AddEvent(ATTACK1_EFFECT, Attack1Effect);
        _animationEventHandler.AddEvent(ATTACK_END_CALLBACK, AttackEndCallback);
    }

    private void SetEffectTransformValue1()
    {
        Transform visualEffect = _visualEffect.transform;
        
        visualEffect.localRotation = Quaternion.Euler(_rotation);
        visualEffect.localPosition = _offset;
        visualEffect.localScale = _scale;
    }

    private void Attack1Effect()
    {
        SetEffectTransformValue1();
        _visualEffect.gameObject.SetActive(true);
    }

    private void AttackEndCallback()
    {
        _visualEffect.gameObject.SetActive(false);
    }
    
    public void InitializeComponent(EntityComponentController componentController)
    {
        _animationEventHandler = componentController.GetEntityComponent<AnimationEventHandler>();
    }
}
