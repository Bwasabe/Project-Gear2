using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterSkillUI : MonoBehaviour
{
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

    public void SetSkillBase(CharacterSkillBase skillBase)
    {
        _characterSkillBase = skillBase;
    }







}
