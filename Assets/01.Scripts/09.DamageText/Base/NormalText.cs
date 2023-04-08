using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NormalText : BaseDamageText
{
    [SerializeField]
    private NormalTextData _textData;

    public override BaseDamageTextData TextData{
        get {
            return _textData;
        }
        set {
            _textData = value as NormalTextData;
        }

    }

    public override BaseDamageText ShowText()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _text.transform.DOLocalMoveY( _textData.TextHeight,_textData.AnimationDuration)
        );

        sequence.Join(
            _text.DOFade(_textData.TextEndAlpha, _textData.AnimationDuration)
        );

        sequence.AppendCallback(ResetText);

        return this;
    }
    

}

[Serializable]
public class NormalTextData : BaseDamageTextData
{
    [field: SerializeField]
    public float TextHeight{ get; private set; } = 1f;
    [field: SerializeField]
    public float AnimationDuration{ get; private set; } = 0.5f;

    [field: SerializeField]
    public float TextEndAlpha{ get;private set; } = 0f;
}
