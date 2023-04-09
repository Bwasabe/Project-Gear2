using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHeal : CharacterSkillBase
{
    [SerializeField]
    private GameObject _healParticle;

    [SerializeField]
    private float _heal;

    private CharacterDamaged _characterDamaged;

    private CharacterSkillController _skillController;

    private Transform _root;
    public void InitializeComponent(EntityComponentController componentController)
    {
        _skillController = componentController.GetEntityComponent<CharacterSkillController>();
        _characterDamaged = componentController.GetEntityComponent<CharacterDamaged>();
    }

    private void OnEnable()
    {
        PlayerSkillUIManager.Instance.AddSkill(this, 0);
    }

    public override void ExecuteSkill()
    {
        if (!_skillController.IsCanMpEnough(_mp)) return;
        _skillController.ScaleUp = true;

        _skillController.UseMp(_mp);

        PoolManager.Instantiate(_healParticle.gameObject, transform.position, Quaternion.identity);

        _characterDamaged.Damaged(-_heal, TextType.PlayerHeal);
    }


}
