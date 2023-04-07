using System.Collections.Generic;

public class BT_Sequence : BT_Node
{
    public BT_Sequence(BehaviourTree tree, List<BT_Node> children) : base(tree, children) {}

    protected override void OnUpdate()
    {
        foreach (BT_Node child in _children)
        {
            Result result = child.Execute();

            switch (result)
            {
            case Result.SUCCESS:
                _nodeResult = Result.SUCCESS;
                continue;
            case Result.FAILURE:
                _nodeResult = Result.FAILURE;
                UpdateState = UpdateState.Exit;
                return;

            }
        }
    }
}
