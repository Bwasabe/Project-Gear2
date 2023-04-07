using System.Collections.Generic;
using UnityEngine;

public class ChortDistanceCondition : BT_Condition
{
    private ChortVariable _variable;
    public ChortDistanceCondition(BehaviourTree tree, List<BT_Node> children) : base(tree, children)
    {
        _variable = tree.Variable as ChortVariable;
    }
    

    protected override void OnUpdate()
    {
        Transform closestCharacter = CharacterManager.Instance.GetClosestCharacter(_tree.transform);

        float distance = Vector3.Distance(closestCharacter.position, _tree.transform.position);

        _nodeResult = SetNodeResultByDistance(distance);
    }

    private Result SetNodeResultByDistance(float distance) => distance switch
    {
        _ when distance <= _variable.AttackRange => _children[0].Execute(),
        _ when distance <= _variable.TargetFindRange => _children[1].Execute(),
        _ => _children[2].Execute(),
    };

    private void Attack()
    {
        
    }
}

public partial class ChortVariable
{
    [field:SerializeField]
    public float TargetFindRange{ get; private set; }
    
    [field:SerializeField]
    public float AttackRange{ get; private set; }
}
