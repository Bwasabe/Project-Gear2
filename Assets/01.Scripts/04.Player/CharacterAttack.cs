using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : BaseEntityAttack, IUpdateAble, ICharacterComponentAble
{
    [SerializeField]
    private float _findCooldown = 0.1f;
    [SerializeField]
    private float _attackRange = 1f;
 
    
    private CharacterAnimation _characterAnimation;
    private CharacterAnimationEventHandler _characterAnimationEventHandler;
    private CharacterStateController _stateController;

    private const string ATTACK_END_CALLBACK = "AttackEndCallback";

    public Transform Target{ get; private set; }
    
    public void InitializePlayerComponent(CharacterComponentController componentController)
    {
        _characterAnimation = componentController.GetPlayerComponent<CharacterAnimation>();
        _characterAnimationEventHandler = componentController.GetPlayerComponent<CharacterAnimationEventHandler>();
        _stateController = componentController.GetPlayerComponent<CharacterStateController>();
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
        _characterAnimationEventHandler.AddEvent("AttackEndCallback", AttackEndCallback);
        
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
        // // TODO: 적을 찾아 공격
        CheckDistance();
    }

    private void CheckDistance()
    {
        if(Target == null) return;
        
        if(Vector3.Distance(Target.position, transform.position) <= _attackRange)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _stateController.AddState(CharacterState.Attack);
        _characterAnimation.TrySetAnimationState(PlayerAnimationState.Attack);
    }

    private void AttackEndCallback()
    {
        Debug.Log("EndCallback");
        _stateController.RemoveState(CharacterState.Attack);
        _characterAnimation.TrySetAnimationState(PlayerAnimationState.Idle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if((collisionGameObject.layer & 1 << _hitLayer) <= 0) return;
        Debug.Log((collisionGameObject.layer & 1 << _hitLayer));
        Debug.Log(collisionGameObject.layer);
        Debug.Log(_hitLayer);

        if(collisionGameObject.TryGetComponent<BaseEntityDamaged>(out BaseEntityDamaged damaged))
        {
            damaged.Damaged(_atk);
        }

    }

}
