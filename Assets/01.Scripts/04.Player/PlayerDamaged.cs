using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerDamaged : BaseEntityDamaged
{
    [SerializeField]
    private float _flickerTime = 0.1f;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start() {
        base.Start();
    }

    private void OnDamageSpriteFlicker()
    {
        if(_hp > 0)
        {
            // TODO: Shader Graph or Sprite Mask로 Fade yoyo
        }
    }
    
}
