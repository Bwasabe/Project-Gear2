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
    
    
    private AnimationCtrl<CharacterAnimationState> _animationController;
    private AnimationEventHandler _animationEventHandler;
    private CharacterStateController _characterStateController;
    private CharacterAnimationController _characterAnimationController;

    private const string ATTACK_END_CALLBACK = "AttackEndCallback";
    
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
        SoundManager.Instance.Play(AudioType.SFX, _attackSound);

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
            SoundManager.Instance.Play(AudioType.SFX, _hitSound);
            damaged.Damaged(_atk, Define.GetRandomEnum<TextType>(0, (int)TextType.PlayerDamaged));
        }

    }

}
