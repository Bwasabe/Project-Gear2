using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceCondition : BT_Condition
{
    private enum ConditionChild
    {
        None = -1,
        AttackLeaf = 0,
        ChaseLeaf = 1,
        IdleOrMoveCondition = 2
    }
    
    private readonly EnemyVariable _variable;

    private readonly Enemy _enemy;
    
    private ConditionChild _executeChild = ConditionChild.None;
    public EnemyDistanceCondition(Enemy tree, List<BT_Node> children) : base(tree, children)
    {
        _enemy = tree;
        _variable = tree.Variable as EnemyVariable;
    }
    
    
    protected override void OnUpdate()
    {
        if(_variable.Target is null)
        {
            _children[(int)ConditionChild.IdleOrMoveCondition].Execute();

            return;
        }
        
        float distance = Vector3.Distance(_variable.Target.transform.position, _tree.transform.position);

        _nodeResult = SetNodeResultByDistance(distance);

    }

    private NodeResult ExecuteChild(ConditionChild child)
    {
        if(CheckExecuteChildIsChanged(child))
        {
            _enemy.OnDistanceLeafChanged?.Invoke();
        }
        
        return _children[(int)child].Execute();
    }

    private bool CheckExecuteChildIsChanged(ConditionChild child)
    {
        int executeChildInt = (int)_executeChild;
        int childInt = (int)child;


        if(executeChildInt != childInt)
        {
            _executeChild = child;
            return true;
        }
        else
            return false;

    }

    private NodeResult SetNodeResultByDistance(float distance) => distance switch
    {
        _ when distance <= _variable.AttackRange => ExecuteChild(ConditionChild.AttackLeaf), // Attack
        _ when distance <= _variable.TargetFindRange => ExecuteChild(ConditionChild.ChaseLeaf), // Chase
        _ => ExecuteChild(ConditionChild.IdleOrMoveCondition), // IdleOrMoveCondition
    };

}

public partial class EnemyVariable
{
    [field:SerializeField]
    public float TargetFindRange{ get; private set; }
    
    [field:SerializeField]
    public float AttackRange{ get; private set; }

    
}
