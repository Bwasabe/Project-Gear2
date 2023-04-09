using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KnightScaleUp : CharacterSkillBase, IGetComponentAble
{
    [SerializeField]
    private float _scaleMultiple = 5f;

    [SerializeField]
    private float _scaleAnimationDuration = 1f;

    [SerializeField]
    private float _ScaleUpDuration = 3f;

    private CharacterAttack _characterAttack;

    private CharacterSkillController _skillController;
    
    private Transform _root;
    public void InitializeComponent(EntityComponentController componentController){
        _skillController = componentController.GetEntityComponent<CharacterSkillController>();
        _characterAttack = componentController.GetEntityComponent<CharacterAttack>();
    }

    private void OnEnable() {
        PlayerSkillUIManager.Instance.AddSkill(this, 0);
    }

    private void Start() {
        _root = transform.root;
    }


    public override void ExecuteSkill()
    {
        if(!_skillController.IsCanMpEnough(_mp))return;
        _skillController.ScaleUp = true;

        _skillController.UseMp(_mp);

        StartCoroutine(ScaleUp());
    }

    private IEnumerator ScaleUp()
    {
        _root.DOScale(Vector3.one * _scaleMultiple, _scaleAnimationDuration);
        yield return Yields.WaitForSeconds(_scaleAnimationDuration);
        _characterAttack.ScaleUp(_scaleMultiple);

        yield return Yields.WaitForSeconds(_ScaleUpDuration);

        _characterAttack.ScaleUp(1/_scaleMultiple);
        _root.DOScale(Vector3.one, _scaleAnimationDuration);
        yield return Yields.WaitForSeconds(_scaleAnimationDuration);

    }
}
