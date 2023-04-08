using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseLeaf : BT_Node
{
    private readonly EnemyVariable _variable;

    private readonly Enemy _enemy;
    public EnemyChaseLeaf(Enemy tree, List<BT_Node> children = null) : base(tree, children)
    {
        _enemy = tree;
        _variable = tree.Variable as EnemyVariable;

        _enemy.OnDistanceLeafChanged += OnLeafChanged;
    }

    private void OnLeafChanged()
    {
        UpdateState = UpdateState.None;
    }

    protected override void OnEnter()
    {
        Debug.Log("ChaseEnter");
        _variable.AnimationController.SetAnimationState(EnemyAnimationState.Move);

        UpdateState = UpdateState.Update;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        Vector2 dir = (_variable.Target.transform.position - _tree.transform.position).normalized;
        
        Flip();
        _variable.Rigidbody2D.velocity = dir * _variable.ChaseSpeed;
    }
    
    private void Flip()
    {
        if(_variable.Rigidbody2D.velocity.x < 0)
            _tree.FlipLeft();
        else
            _tree.FlipRight();
    }
}

public partial class EnemyVariable
{
    [field: SerializeField]
    public float ChaseSpeed{ get; private set; } = 1f;
}
