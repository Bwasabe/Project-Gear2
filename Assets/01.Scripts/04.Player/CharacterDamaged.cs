using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterDamaged : BaseEntityDamaged, IGetComponentAble
{

    public override void Damaged(float damage, TextType textType)
    {
        base.Damaged(damage, textType);
    }
}
