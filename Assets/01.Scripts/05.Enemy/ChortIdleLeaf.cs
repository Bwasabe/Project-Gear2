using System.Collections.Generic;
using UnityEngine;

public class ChortIdleLeaf : BT_Node
{
    private readonly ChortVariable _variable;

    private float _duration;
    private float _timer;
    
    public ChortIdleLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _variable = tree.Variable as ChortVariable;
    }

    protected override void OnEnter()
    {

        _duration = Random.Range(_variable.IdleDurationMin, _variable.IdleDurationMax);
        
        _variable.AnimationController.TrySetAnimationState(EnemyAnimationState.Idle);

        UpdateState = UpdateState.Update;
        _variable.Rigidbody2D.velocity = Vector2.zero;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        
        if(_timer > _duration)
        {
            _timer = 0f;
            _variable.IsIdleing = false;
            UpdateState = UpdateState.None;
        }
    }
}

public partial class ChortVariable
{
    [field: SerializeField]
    public float IdleDurationMax{ get; private set; } = 1f;
    [field: SerializeField]
    public float IdleDurationMin{ get; private set; } = 0.2f;

    public bool IsIdleing{ get; set; } = true;

}
