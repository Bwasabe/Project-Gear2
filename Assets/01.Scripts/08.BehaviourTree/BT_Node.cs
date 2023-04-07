
using System.Collections.Generic;

public enum Result
{
    SUCCESS,
    FAILURE,
}

public enum UpdateState
{
    None,
    Enter,
    Update,
    Exit
}

public abstract class BT_Node
{
    protected Result _nodeResult = Result.FAILURE;

    protected BehaviourTree _tree;

    public UpdateState UpdateState{ get; set; } = UpdateState.None;

    protected List<BT_Node> _children;

    protected BT_Node(BehaviourTree tree, List<BT_Node> children = null)
    {
        _tree = tree;
        _children = children;
    }

    public virtual Result Execute()
    {
        if(CheckUpdateState(UpdateState.None))
        {
            EnterNode();
        }
        
        if (CheckUpdateState(UpdateState.Enter))
        {
            OnEnter();
        }

        if (CheckUpdateState(UpdateState.Update))
        {
            OnUpdate();
        }

        if (CheckUpdateState(UpdateState.Exit))
        {
            OnExit();
        }

        return _nodeResult;
    }
    
    protected virtual void OnEnter()
    {
        UpdateState = UpdateState.Update;
    }
    
    protected virtual void OnUpdate()
    {
        
    }
    
    protected virtual void OnExit()
    {
        UpdateState = UpdateState.None;
    }
    
    private void EnterNode()
    {
        UpdateState = UpdateState.Enter;

        if(_children == null) return;

        foreach (BT_Node child in _children)
        {
            if(child.CheckUpdateState(UpdateState.None))
            {
                child.UpdateState = UpdateState.Enter;
                child.EnterNode();
            }
        }
    }

    protected bool CheckUpdateState(UpdateState updateState)
    {
        int currentState = (int)UpdateState;
        int checkState = (int)updateState;

        return currentState == checkState;
    }
}
