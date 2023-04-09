using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHeal : CharacterSkillBase, IGetComponentAble
{
    [SerializeField]
    private float _heal;

    private CharacterDamaged _characterDamaged;


    private Transform _root;
    public void InitializeComponent(EntityComponentController componentController)
    {
        _characterDamaged = componentController.GetEntityComponent<CharacterDamaged>();
    }

    protected override void OnEnable()
    {
        PlayerSkillUIManager.Instance.AddSkill(this, 2);

        base.OnEnable();
    }


    public override void ExecuteSkill()
    {
        _characterDamaged.Damaged(-_heal, TextType.PlayerHeal);
    }

}
