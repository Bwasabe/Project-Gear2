using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterDamaged : BaseEntityDamaged, IGetComponentAble
{
    // TODO: 데미지 텍스트 띄우기

    public override void Damaged(float damage, TextType textType)
    {
        base.Damaged(damage, textType);
        Debug.Log(_hp);
    }
}
