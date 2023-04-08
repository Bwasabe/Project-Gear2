using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLeaf : BT_Node
{
    private Vector2 _dir;

    private float _duration;

    private readonly EnemyVariable _variable;

    private bool _isFirstEnterMove = true;
    public EnemyMoveLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _variable = tree.Variable as EnemyVariable;
    }

    protected override void OnEnter()
    {
        if(_isFirstEnterMove)
        {
            _duration = Random.Range(_variable.EnterMoveDurationMin, _variable.EnterMoveDurationMax);
            _isFirstEnterMove = false;
        }
        else
        {
            _duration = Random.Range(_variable.MoveDurationMin, _variable.MoveDurationMax);
        }
        

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        _dir = new Vector2(randomX, randomY).normalized;
        
        _variable.AnimationController.TrySetAnimationState(EnemyAnimationState.Move);
        
        UpdateState = UpdateState.Update;
    }

    protected override void OnUpdate()
    {
        _variable.Timer += Time.deltaTime;
        if(_variable.Timer > _duration)
        {
            _variable.Timer = 0f;
            
            UpdateState = UpdateState.Exit;
        }
        else
        {
            Flip();
            _variable.Rigidbody2D.velocity = _dir * _variable.MoveSpeed;
        }
    }

    private void Flip()
    {
        if(_variable.Rigidbody2D.velocity.x < 0)
            _tree.FlipLeft();
        else
            _tree.FlipRight();
    }


    protected override void OnExit()
    {
        _variable.IsIdleing = true;
        _variable.Timer = 0f;
        UpdateState = UpdateState.None;
    }

    public override void ResetNode()
    {
        
        base.ResetNode();
    }
}

public partial class EnemyVariable
{
    [field:SerializeField]
    public float MoveDurationMax{ get; private set; } = 0.5f;

    [field: SerializeField]
    public float MoveDurationMin{ get; private set; } = 0.2f;
    
    [field:SerializeField]
    public float EnterMoveDurationMax{ get; private set; } = 2f;

    [field: SerializeField]
    public float EnterMoveDurationMin{ get; private set; } = 1.5f;

    [field: SerializeField]
    public float MoveSpeed{ get; private set; } = 1f;

    public float Timer{ get; set; } = 0f;

}