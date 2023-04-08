using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : BehaviourTree, IGetComponentAble, IPoolReturnAble, IPoolInitAble
{
    [SerializeField]
    private EnemyVariable _variable;

    
    public Action OnDistanceLeafChanged;

    private EnemyAnimationController _animationController;
    
    public override BT_Variable Variable{
        get {
            return _variable;
        }
        set {
            _variable = value as EnemyVariable;
        }
    }
    
    private void Awake()
    {
        _variable.Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        
        _variable.AnimationController = _animationController.AnimationCtrl;
    }
    
    public void InitializeComponent(EntityComponentController componentController)
    {
        _animationController = componentController.GetEntityComponent<EnemyAnimationController>();
        
        AnimationEventHandler eventHandler = componentController.GetEntityComponent<AnimationEventHandler>();
        _variable.AnimationEventHandler = eventHandler;
    }
    
   

    private IEnumerator FindTarget()
    {
        while (!_variable.IsStopFindTarget)
        {
            Transform targetTrm = CharacterManager.Instance.GetClosestCharacter(transform);

            if(targetTrm is null)
            {
                _variable.Target = null;
                yield return null;

                continue;
            }

            if(_variable.Target == null || targetTrm != _variable.Target.transform)
            {
                _variable.Target = targetTrm.GetComponent<CharacterDamaged>();
            }
            yield return Yields.WaitForSeconds(_variable.FindCooldown);
        }
    }

    


    protected override BT_Node SetupTree()
    {
        return new EnemyDistanceCondition(this, new() {

                new EnemyAttackLeaf(this),
                new EnemyChaseLeaf(this),
                
                new EnemyIdleOrMoveCondition(this, new() {
                    new EnemyIdleLeaf(this),
                    new EnemyMoveLeaf(this),
                }),
                
            });
    }

    public void Return()
    {
        Debug.Log("Return");
        _variable.Rigidbody2D.velocity = Vector2.zero;
        _variable.Target = null;
        _root.ResetNode();
    }
    public void Init()
    {
        StartCoroutine(FindTarget());
    }
}

public partial class EnemyVariable
{
    public AnimationCtrl<EnemyAnimationState> AnimationController{ get; set; }
    public AnimationEventHandler AnimationEventHandler{ get; set; }
    public Rigidbody2D Rigidbody2D{ get; set; }
    public CharacterDamaged Target{ get; set; }
    

    [field: SerializeField]
    public float FindCooldown{ get; private set; } = 0.1f;
    
    public bool IsStopFindTarget{ get; set; }
    
    public bool IsAllCharacterIsDied{ get; set; }
}