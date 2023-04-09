using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : BaseEntityDamaged, IGetComponentAble, IPoolReturnAble
{


    protected override void Start()
    {
        base.Start();
        OnDamageTaken += ShowDamageText;
    }
    private void ShowDamageText(float damage, TextType textType)
    {
            DamageTextManager.Instance.GetDamageText(textType, damage.ToString()).SetPosition(transform.position).ShowText();

    }
    public void Return()
    {
        Hp = _maxHp;
    }
}
