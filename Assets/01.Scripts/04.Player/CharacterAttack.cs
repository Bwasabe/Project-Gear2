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
    
    
    private AnimationCtrl<PlayerAnimationState> _animationController;
    private AnimationEventHandler _animationEventHandler;
    private CharacterStateController _characterStateController;

    private const string ATTACK_END_CALLBACK = "AttackEndCallback";
    
    public Transform Target{ get; private set; }
    
    public void InitializeComponent(CharacterComponentController componentController)
    {
        CharacterAnimationController characterAnimationController = componentController.GetPlayerComponent<CharacterAnimationController>();
        _animationController = characterAnimationController.AnimationCtrl;
        
        _animationEventHandler = componentController.GetPlayerComponent<AnimationEventHandler>();
        _characterStateController = componentController.GetPlayerComponent<CharacterStateController>();
    }

    private void OnEnable()
    {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance.UnRegisterObject(this);
    }

    private void Start()
    {
        _animationEventHandler.AddEvent(ATTACK_END_CALLBACK, AttackEndCallback);
        
        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            Transform closestEnemy = EnemyManager.Instance.GetClosestEnemy(transform);

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
        if(Target == null) return;

        Vector3 rootPos = _characterStateController.transform.position;
        
        if(Vector3.Distance(Target.position, rootPos) <= _attackRange)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _characterStateController.AddState(CharacterState.Attack);
        _animationController.TrySetAnimationState(PlayerAnimationState.Attack);
    }

    private void AttackEndCallback()
    {
        _characterStateController.RemoveState(CharacterState.Attack);
        _animationController.TrySetAnimationState(PlayerAnimationState.Idle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if((collisionGameObject.layer & 1 << _hitLayer) <= 0) return;

        if(collisionGameObject.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            damaged.Damaged(_atk);
        }

    }

}
