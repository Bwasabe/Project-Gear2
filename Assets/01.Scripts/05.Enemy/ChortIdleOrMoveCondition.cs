using System.Collections.Generic;
using UnityEngine;

public class ChortIdleOrMoveCondition : BT_Condition
{
    private ChortVariable _variable;
    public ChortIdleOrMoveCondition(BehaviourTree tree, List<BT_Node> children) : base(tree, children)
    {
        _variable = tree.Variable as ChortVariable;
        
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
