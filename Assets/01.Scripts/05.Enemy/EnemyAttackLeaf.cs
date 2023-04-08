using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackLeaf : BT_Node
{
    private const string CHORT_ATTACK = "ChortAttack";
    private readonly EnemyVariable _variable;

    public EnemyAttackLeaf(BehaviourTree tree, List<BT_Node> children = null) : base(tree, children)
    {
        _variable = tree.Variable as EnemyVariable;
        
        _variable.AnimationEventHandler.AddEvent(CHORT_ATTACK, AttackTarget);
    }

    protected override void OnEnter()
    {
        _variable.Rigidbody2D.velocity = Vector2.zero;
        
        Flip();
        
        _variable.AnimationController.TrySetAnimationState(EnemyAnimationState.Attack);

        UpdateState = UpdateState.None;
    }

    private void AttackTarget()
    {
        if(_variable.Target == null) return;

        DamageTextManager.Instance.GetDamageText(TextType.PlayerDamaged, (-_variable.Damage).ToString()).SetPosition(_variable.Target.transform.position).ShowText();
        _variable.Target.Damaged(_variable.Damage, TextType.PlayerDamaged);
    }

    private void Flip()
    {
        if(_variable.Target is null) return;
        
        if(_variable.Target.transform.position.x < _tree.transform.position.x)
            _tree.FlipLeft();
        else
            _tree.FlipRight();
    }
    

}

public partial class EnemyVariable
{
    [field: SerializeField]
    public float Damage{ get; private set; } = 1;
    // public bool IsAttack{ get; set; }
}
