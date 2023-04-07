using System.Collections.Generic;
using UnityEngine;

public class ChortMoveLeaf : BT_Node
{
    private Vector2 _dir;

    private float _duration;
    private float _timer;

    private readonly ChortVariable _variable;
    public ChortMoveLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _variable = tree.Variable as ChortVariable;
    }

    protected override void OnEnter()
    {
        _duration = Random.Range(_variable.MoveDurationMin, _variable.MoveDurationMax);

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        _dir = new Vector2(randomX, randomY).normalized;
        
        _variable.AnimationController.TrySetAnimationState(EnemyAnimationState.Move);
        
        UpdateState = UpdateState.Update;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer > _duration)
        {
            _timer = 0f;
            
            UpdateState = UpdateState.Exit;
        }
        else
        {
            _variable.Rigidbody2D.velocity = _dir * _variable.MoveSpeed;
        }
    }

    protected override void OnExit()
    {
        _variable.IsIdleing = true;
        UpdateState = UpdateState.None;
    }
}

public partial class ChortVariable
{
    [field:SerializeField]
    public float MoveDurationMax{ get; private set; } = 0.5f;

    [field: SerializeField]
    public float MoveDurationMin{ get; private set; } = 0.2f;

    [field: SerializeField]
    public float MoveSpeed{ get; private set; } = 1f;

}