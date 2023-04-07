using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : BaseEntityDamaged
{
    [SerializeField]
    private float _flickerTime = 0.1f;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start() {
        base.Start();
        
        OnDamageTaken += OnDamageSpriteFlicker;
    }

    private void OnDamageSpriteFlicker(float damage)
    {
        if(_hp > 0)
        {
            // TODO: Shader Graph or Sprite Mask로 Fade yoyo
        }
    }
}
