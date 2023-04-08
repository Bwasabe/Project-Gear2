using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriticalDamageText : BaseDamageText
{
    [SerializeField]
    private CriticalTextData _textData;
    
    
    public override BaseDamageTextData TextData{
        get {
            return _textData;
        }
        set {
            _textData = value as CriticalTextData;
        }
    }


    public override BaseDamageText ShowText()
    {
        
        _text.transform.localPosition = _textData.TextPos;

        _text.transform.localScale = _textData.TextScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _text.transform.DOScale(_textScale, )
        );

        sequence.Append(
            _text.transform.DOLocalMoveY(dir.y, _textData.UpDownDuration * 0.5f) //.SetEase(Ease.OutElastic)
        );

        sequence.AppendInterval(_textData.ScaleDelay);

        sequence.Join(
            _text.transform.DOScale(Vector3.zero, _textData.ScaleDuration) //.SetEase(Ease.InElastic)
        );

        sequence.Join(
            _text.DOFade(_textData.TextEndAlpha, _textData.AlphaDuration)
        );


        sequence.AppendCallback(ResetText);

        return this;
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        _text.alpha = 1f;
        _text.transform.localScale = _textScale;
    }
}

[Serializable]
public class CriticalTextData : BaseDamageTextData
{
    [field: SerializeField] public Vector3 TextScale = Vector3.one * 0.3f;
    [field: SerializeField] public float ScaleDuration{ get; private set; } = 0.15f;

    
    [field: SerializeField] public Vector3 TextPos = new Vector3(0f, 1f, 0f);
    

    [field: SerializeField] public float TextEndAlpha{ get; private set; } = 0.2f;
    [field: SerializeField] public float AlphaDuration{ get; private set; } = 0.5f;
}
