using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightLightningSkill : CharacterSkillBase, IGetComponentAble
{
    [SerializeField]
    private LightningParticle _lightningParticle;

    [SerializeField]
    private Vector2 _drag;

    [SerializeField]
    private float _dashDistance;

    [SerializeField]
    private float _lightningSound;

    private CharacterAnimationController _characterAnimationController;
    private CharacterSkillController _skillController;
    private CharacterDamaged _characterDamaged;
    private CharacterMove _characterMove;

    private Rigidbody2D _rb;

    private Vector2 _caclVelocity;

    private readonly int SKILL1_HASH = Animator.StringToHash("Skill1");
    private readonly int SKILL1_END_HASH = Animator.StringToHash("Skill1End");


    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();

    }

    private void OnEnable() {
        PlayerSkillUIManager.Instance.AddSkill(this, 0);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteSkill();
            //_lightningParticle.SetParticleRotationAndPositon(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg, _rb.velocity.normalized);
        }
    }

    public void InitializeComponent(EntityComponentController componentController)
    {
        _characterDamaged = componentController.GetEntityComponent<CharacterDamaged>();
        _characterAnimationController = componentController.GetEntityComponent<CharacterAnimationController>();
        _characterMove = componentController.GetEntityComponent<CharacterMove>();

        _skillController = componentController.GetEntityComponent<CharacterSkillController>();
    }

    public override void ExecuteSkill()
    {
        if (!_skillController.IsCanMpEnough(_mp)) return;

        _characterAnimationController.AnimationCtrl.Animator.SetTrigger(SKILL1_HASH);

        _skillController.UseMp(_mp);

        Vector2 dir = _rb.velocity.normalized;

        // float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // LightningParticle lightObj = PoolManager.Instantiate(_lightningParticle.gameObject).GetComponent<LightningParticle>();

        // lightObj.SetParticleRotationAndPositon(rotation, _characterDamaged.transform.position);

        _characterMove.enabled = false;

        if (dir == Vector2.zero)
        {
            dir = Vector2.right;
        }


        _caclVelocity += Vector2.Scale(dir, _dashDistance * new Vector2(
            Mathf.Log(1f / (Time.deltaTime * _drag.x + 1)) / -Time.deltaTime,
            Mathf.Log(1f / (Time.deltaTime * _drag.y + 1)) / -Time.deltaTime
        ));
        _rb.velocity = _caclVelocity;

        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        while (_caclVelocity.magnitude > 0.5f)
        {
            _caclVelocity /= _drag * Time.deltaTime + Vector2.one;
            _rb.velocity = _caclVelocity;
            yield return null;
        }
        _characterMove.enabled = true;
        _characterAnimationController.AnimationCtrl.Animator.SetTrigger(SKILL1_END_HASH);
        _lightningParticle.gameObject.SetActive(false);

    }
}
