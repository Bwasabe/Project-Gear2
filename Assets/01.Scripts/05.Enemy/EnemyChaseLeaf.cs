using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyChaseLeaf : BT_Node
{
    private readonly EnemyVariable _variable;

    public EnemyChaseLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _variable = tree.Variable as EnemyVariable;
    }

    protected override void OnEnter()
    {
        //_variable.ExMark.transform.DOScale(Vector3.one * 0.1f,_variable.ExMarkScaleDuration).SetEase(Ease.OutElastic);


        UpdateState = UpdateState.Update;
    }

    protected override void OnUpdate()
    {
        _variable.AnimationController.SetAnimationState(EnemyAnimationState.Move);
        base.OnUpdate();
        Vector2 dir = (_variable.Target.transform.position - _tree.transform.position).normalized;
        
        Flip();
        _variable.Rigidbody2D.velocity = dir * _variable.ChaseSpeed;
    }

    public override void ResetNode()
    {
        UpdateState = UpdateState.None;
        // _variable.ExMark.localScale = Vector3.zero;
        base.ResetNode();
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
    [field: SerializeField] public float ChaseSpeed{ get; private set; } = 1f;
    
    // [field:SerializeField] public Transform ExMark{ get; private set; }
    // [field: SerializeField] public float ExMarkScaleDuration{ get; private set; } = 0.1f;
}
