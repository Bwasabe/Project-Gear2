using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : BaseEntityAttack, IUpdateAble, IGetComponentAble
{
    [SerializeField]
    private float _findCooldown = 0.1f;
    [SerializeField]
    private float _attackRange = 1f;
    [SerializeField]
    private AudioClip _attackSound;
    [SerializeField]
    private AudioClip _hitSound;

    [SerializeField]
    private float _criticalPercent = 50;

    [SerializeField]
    private float _criticalDamage = 2f;
    
    
    private AnimationCtrl<CharacterAnimationState> _animationController;
    private AnimationEventHandler _animationEventHandler;
    private CharacterStateController _characterStateController;
    private CharacterAnimationController _characterAnimationController;

    private const string ATTACK_END_CALLBACK = "AttackEndCallback";
    private const string ATTACK_START = "AttackStart";

    private bool _isCanPlaySound = false;

    public Transform Target{ get; private set; }
    
    public void InitializeComponent(EntityComponentController componentController)
    {
        _characterAnimationController = componentController.GetEntityComponent<CharacterAnimationController>();
        
        
        _animationEventHandler = componentController.GetEntityComponent<AnimationEventHandler>();
        _characterStateController = componentController.GetEntityComponent<CharacterStateController>();
    }

    private void OnEnable()
    {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.UnRegisterObject(this);
    }

    private void Start()
    {
        _animationEventHandler.AddEvent(ATTACK_END_CALLBACK, AttackEndCallback);
        _animationEventHandler.AddEvent(ATTACK_START, AttackStart);

        _animationController = _characterAnimationController.AnimationCtrl;

        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            Transform closestEnemy = EnemySpawnManager.Instance.GetClosestEnemy(transform);

            Target = closestEnemy;

            yield return Yields.WaitForSeconds(_findCooldown);
        }
    }

    public void OnUpdate()
    {
        CheckDistance();
    }

    public void ScaleUp(float multiple)
    {
        _atk *= multiple;
        _attackRange *= multiple;
        
    }

    private void CheckDistance()
    {
        if(Target is null) return;

        Vector3 rootPos = _characterStateController.transform.position;
        
        if(Vector3.Distance(Target.position, rootPos) <= _attackRange)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _characterStateController.AddState(CharacterState.Attack);
        _animationController.TrySetAnimationState(CharacterAnimationState.Attack);

    }
    private void AttackStart()
    {
        SoundManager.Instance.Play(AudioType.SFX, _attackSound);
        _isCanPlaySound = true;
    }

    private void AttackEndCallback()
    {
        _characterStateController.RemoveState(CharacterState.Attack);
        _animationController.TrySetAnimationState(CharacterAnimationState.Idle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if((collisionGameObject.layer & 1 << _hitLayer) <= 0) return;

        if(collisionGameObject.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            if(_isCanPlaySound)
            {
                SoundManager.Instance.Play(AudioType.SFX, _hitSound);
                _isCanPlaySound = false;
            }

            float criticalRandom = UnityEngine.Random.Range(0f, 100f);

            Vector3 dir = other.transform.position - transform.position;
            dir.Normalize();

            CameraManager.Instance.CameraShake(dir , 0.05f);
            
            if(criticalRandom <= _criticalPercent)
                damaged.Damaged(_atk * _criticalDamage,TextType.EnemyCritical);
            else
                damaged.Damaged(_atk,TextType.EnemyDamage);
            
        }

    }

}
