using System;
using System.Collections.Generic;

public class BT_Selector : BT_Node
{
    public BT_Selector(BehaviourTree tree, List<BT_Node> children) : base(tree, children) {}

    protected override void OnUpdate()
    {
        foreach (BT_Node child in _children)
        {
            Result result = child.Execute();

            switch (result)
            {
                case Result.SUCCESS:
                    _nodeResult = Result.SUCCESS;
                    UpdateState = UpdateState.Exit;
                    return;
                case Result.FAILURE:
                    _nodeResult = Result.FAILURE;

                    continue;
            }
        }
    }
    
}