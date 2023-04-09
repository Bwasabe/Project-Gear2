using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerSkillUI : MonoBehaviour
{
    [SerializeField]
    private Image _skillIcon;

    [SerializeField]
    private Image _background;

    [SerializeField]
    private Image _cooldownImage;

    private CharacterSkillBase _characterSkillBase;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

    }
    

    private void Start() {
        _button.onClick.AddListener(OnClickSkillUI);
    }

    private void OnClickSkillUI()
    {
        _characterSkillBase.ExecuteSkill();
    }

    private void SetCooldownImageFillAmount(float percent)
    {
        _cooldownImage.fillAmount = percent;
    }

    public void SetSkillBase(CharacterSkillBase skillBase)
    {
        if(_characterSkillBase != null)
        {
            _characterSkillBase.OnSkillTimerChanged -= SetCooldownImageFillAmount;
        }

        _characterSkillBase = skillBase;
        _skillIcon.sprite = skillBase.SkillIcon;

        _characterSkillBase.OnSkillTimerChanged += SetCooldownImageFillAmount;
    }

    

}
