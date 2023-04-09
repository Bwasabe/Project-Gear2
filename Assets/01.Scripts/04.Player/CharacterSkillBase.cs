using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterSkillBase : MonoBehaviour, IUpdateAble
{
    [field: SerializeField] protected float _skillCooldown;
    [field: SerializeField] public float CooldownTimer { get; protected set; }

    [field: SerializeField] public Sprite SkillIcon { get; private set; }

    [field: SerializeField] public Color SkillBackgroundColor { get; private set; }



    public event Action<float> OnSkillTimerChanged;

    public bool IsCanUseSkill { get; protected set; } = true;


    public abstract void ExecuteSkill();

    protected virtual void OnEnable()
    {
        UpdateManager.Instance.RegisterObject(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.UnRegisterObject(this);
    }


    public void OnUpdate()
    {
        if (!IsCanUseSkill)
        {
            CooldownTimer -= Time.deltaTime;

            if (CooldownTimer < 0)
            {
                IsCanUseSkill = true;
            }

            OnSkillTimerChanged?.Invoke(CooldownTimer / _skillCooldown);

        }
    }

    public void StartCooldown()
    {
        CooldownTimer = _skillCooldown;
        IsCanUseSkill = false;
    }
}
