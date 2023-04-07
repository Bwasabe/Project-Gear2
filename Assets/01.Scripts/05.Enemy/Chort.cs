using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chort : BehaviourTree, IGetComponentAble
{
    [SerializeField]
    private ChortVariable _chortVariable;

    private void Awake()
    {
        Debug.Log("Awake");
        _chortVariable.Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void InitializeComponent(EntityComponentController componentController)
    {
        EnemyAnimationController animationController = componentController.GetPlayerComponent<EnemyAnimationController>();
        _chortVariable.AnimationController = animationController.AnimationCtrl;
    }
    public override BT_Variable Variable{
        get {
            return _chortVariable;
        }
        set {
            _chortVariable = value as ChortVariable;
        }
    }



    protected override BT_Node SetupTree()
    {
        return new ChortDistanceCondition(this, new() {

                new ChortAttackLeaf(this),
                new ChortChaseLeaf(this),
                
                new ChortIdleOrMoveCondition(this, new() {
                    new ChortIdleLeaf(this),
                    new ChortMoveLeaf(this),
                }),
                
            });
    }

}

public partial class ChortVariable
{
    public AnimationCtrl<EnemyAnimationState> AnimationController{ get; set; }
    public Rigidbody2D Rigidbody2D{ get; set; }
}

public class ChortChaseLeaf : BT_Node
{

    public ChortChaseLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
    }

    protected override void OnEnter()
    {
        Debug.Log("Chase");
        
    }
}

public class ChortAttackLeaf : BT_Node
{
    public ChortAttackLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
    }

    protected override void OnEnter()
    {
        Debug.Log("Attack");
    }
}
