using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteMask : MonoBehaviour, IGetComponentAble, IPoolReturnAble
{
    public void SetMaskScaleByPercent(float percent)
    {
        Vector3 scale = transform.localScale;
        scale.y = percent;
        transform.localScale = scale;
    }
    public void Return()
    {
        transform.localScale = Vector3.one;
    }
}
