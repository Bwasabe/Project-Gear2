using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterDamaged : BaseEntityDamaged, IGetComponentAble
{

    public override void Damaged(float damage, TextType textType)
    {
        if((int)textType == (int)TextType.PlayerHeal)
        {
            DamageTextManager.Instance.GetDamageText(textType, damage.ToString()).SetPosition(transform.position).ShowText();
        }
        base.Damaged(damage, textType);
    }
}
