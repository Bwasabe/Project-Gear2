using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriticalDamageText : RandomPositionText
{
    [SerializeField]
    private CriticalTextData _criticalTextData;

    public override BaseDamageTextData TextData{
        get {
            return _criticalTextData;
        }
        set {
            _criticalTextData = value as CriticalTextData;
        }
    }
    
    public override BaseDamageText ShowText()
    {
        _text.transform.localScale = _criticalTextData.TextScale;
        
        SetRandomPos();
        InitializeDotween();

       
        _sequence.Append(
            _text.transform.DOScale(_textScale,_criticalTextData.ScaleDuration)
        );

        AddAlphaScaleAnimation();
        
        return this;
    }

}

[Serializable]
public class CriticalTextData : RandomPositionTextData
{
    [field: SerializeField] public Vector3 TextScale = Vector3.one * 0.3f;
    [field: SerializeField] public float ScaleDuration{ get; private set; } = 0.15f;

}