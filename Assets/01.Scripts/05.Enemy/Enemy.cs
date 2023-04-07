using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : BehaviourTree, IGetComponentAble
{
    [SerializeField]
    private EnemyVariable _variable;

    
    public Action OnDistanceLeafChanged;
    
    private void Awake()
    {
        _variable.Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (!_variable.IsStopFindTarget)
        {
            Transform targetTrm =CharacterManager.Instance.GetClosestCharacter(transform);

            if(targetTrm is null)
            {
                Debug.Log("모든 캐릭터가 죽음");
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

    public void InitializeComponent(EntityComponentController componentController)
    {
        EnemyAnimationController animationController = componentController.GetEntityComponent<EnemyAnimationController>();
        _variable.AnimationController = animationController.AnimationCtrl;

        AnimationEventHandler eventHandler = componentController.GetEntityComponent<AnimationEventHandler>();
        _variable.AnimationEventHandler = eventHandler;
    }
    
    public override BT_Variable Variable{
        get {
            return _variable;
        }
        set {
            _variable = value as EnemyVariable;
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