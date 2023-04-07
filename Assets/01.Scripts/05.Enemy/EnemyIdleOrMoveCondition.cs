using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleOrMoveCondition : BT_Condition
{
    private EnemyVariable _variable;
    public EnemyIdleOrMoveCondition(BehaviourTree tree, List<BT_Node> children) : base(tree, children)
    {
        _variable = tree.Variable as EnemyVariable;
        
    }

    protected override void OnEnter()
    {
        UpdateState = UpdateState.Update;
    }

    protected override void OnUpdate()
    {
        if(_variable.IsIdleing)
        {
            // IdleNode
            _nodeResult = _children[0].Execute();
        }
        else 
        {
            // MoveNode
            _nodeResult = _children[1].Execute();
        }

        UpdateState = UpdateState.Exit;

    }
}
