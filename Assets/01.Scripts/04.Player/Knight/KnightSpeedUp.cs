using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpeedUp : CharacterSkillBase, IGetComponentAble
{
   [SerializeField]
    private GameObject _speedUpParticle;

    [SerializeField]
    private float _speedUp = 8f;

    [SerializeField]
    private float _speedUpDuration;

    private float _defaultSpeed;
    private CharacterMove _characterMove;

    private Transform _root;
    public void InitializeComponent(EntityComponentController componentController)
    {
        _characterMove = componentController.GetEntityComponent<CharacterMove>();
    }

    private void Start() {
        _defaultSpeed = _characterMove.Speed;
    }

    protected override void OnEnable()
    {
        PlayerSkillUIManager.Instance.AddSkill(this, 0);
        base.OnEnable();
    }

    public override void ExecuteSkill()
    {

        _speedUpParticle.SetActive(true);

        _characterMove.Speed = _speedUp;
        StartCoroutine(SpeedUp());
    }

    private IEnumerator SpeedUp()
    {
        yield return Yields.WaitForSeconds(_speedUpDuration);
        _characterMove.Speed = _defaultSpeed;
        _speedUpParticle.SetActive(false);
    }

}
