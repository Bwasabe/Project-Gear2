using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _currentHealthText;

    [SerializeField]
    private Image _fillImage;

    private BaseEntityDamaged _playerDamaged;

    private void Start() {
        _playerDamaged = CharacterManager.Instance.Knight.GetComponent<BaseEntityDamaged>();

        _playerDamaged.OnDamageTaken += ChangePlayerHpUI;
    }

    private void ChangePlayerHpUI(float _f, TextType _t)
    {
        _fillImage.fillAmount = _playerDamaged.Hp / _playerDamaged.MaxHp;

        _currentHealthText.text = _playerDamaged.Hp.ToString();
    }
}
