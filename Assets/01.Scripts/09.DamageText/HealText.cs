using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealText : RandomPositionText
{
    public override BaseDamageText SetText(string text)
    {
        return base.SetText('+' + text.Substring(1));
    }
}
