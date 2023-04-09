using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KnightAttack2Effect : MonoBehaviour, IGetComponentAble
{
    [Header("Attack2")]
    [field: SerializeField]
    private Vector3 _rotation = new Vector3(-65f, 0f, 0f);
    [SerializeField]
    private Vector3 _offset = new Vector3(0.55f, -0.3f, 0f);
    [SerializeField]
    private Vector3 _scale = new Vector3(-0.4f, 0.2f, 0.25f);

    [SerializeField]
    private VisualEffect _visualEffect;

    private AnimationEventHandler _animationEventHandler;

    
    private const string ATTACK2_EFFECT = "Attack2Effect";
    
    private void Start()
    {
        _animationEventHandler.AddEvent(ATTACK2_EFFECT, Attack2Effect);
    }

    private void SetEffectTransformValue()
    {
        Transform visualEffect = _visualEffect.transform;
        
        visualEffect.localRotation = Quaternion.Euler(_rotation);
        visualEffect.localPosition = _offset;
        visualEffect.localScale = _scale;
    }

    private void Attack2Effect()
    {
        SetEffectTransformValue();
        _visualEffect.gameObject.SetActive(true);
    }

    public void InitializeComponent(EntityComponentController componentController)
    {
        _animationEventHandler = componentController.GetEntityComponent<AnimationEventHandler>();
    }
}
