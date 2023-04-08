using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyFlicker : MonoBehaviour, IGetComponentAble, IPoolInitAble
{
    private readonly int OPACITY = Shader.PropertyToID("_Opacity");
    [SerializeField]
    private float _flickerDuration = 0.1f;

    private Coroutine _flickerCoroutine;

    private Material _material;

    private EnemyDamaged _enemyDamaged;
    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        _material = spriteRenderer.material;
    }
    
    public void InitializeComponent(EntityComponentController componentController)
    {
        _enemyDamaged = componentController.GetEntityComponent<EnemyDamaged>();
    }

    private void Start()
    {
        _enemyDamaged.OnDamageTaken += Hit;
    }

    private void Hit(float damage)
    {
        ResetColor();
        
        _material.SetFloat(OPACITY, 1);
        
        _flickerCoroutine = StartCoroutine(Flicker());
    }

    private void ResetColor()
    {
        if(_flickerCoroutine != null)
        {
            StopCoroutine(_flickerCoroutine);
        }

        _material.SetFloat(OPACITY, 0);
    }

    private IEnumerator Flicker()
    {
        if(_material == null) yield break;

        float start = _material.GetFloat(OPACITY);
        const float end = 0;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / _flickerDuration;

            _material.SetFloat(OPACITY, Mathf.Lerp(start, end, t));

            yield return null;
        }
    }

    public void Init()
    {
        ResetColor();
    }

}
