using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPositionText : BaseDamageText
{
    [SerializeField]
    private RandomPositionTextData _textData;

    public override BaseDamageTextData TextData{
        get {
            return _textData;
        }
        set {
            _textData = value as RandomPositionTextData;
        }
    }

    protected Sequence _sequence;

    public override BaseDamageText ShowText()
    {
        
        SetRandomPos();
        
        InitializeDotween();
        AddAlphaScaleAnimation();
        
        return this;
    }

    protected void InitializeDotween()
    {
        _sequence = DOTween.Sequence();
    }

    protected void AddAlphaScaleAnimation()
    {
        _sequence.AppendInterval(_textData.TextDisplayDuration);
        
        _sequence.Append(
            _text.transform.DOScale(Vector3.zero, _textData.EndScaleDuration)
        );

        _sequence.Join(
            _text.DOFade(_textData.TextEndAlpha, _textData.Alph
        aDuration)
        );


        _sequence.AppendCallback(ResetText);
    }

    protected void SetRandomPos()
    {
        Vector3 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        dir.Normalize();

        float radius = Random.Range(_textData.RadiusMin, _textData.RadiusMax);

        dir *= radius;

        _text.transform.localPosition = dir;
    }

    protected override void ResetValue()
    {
        base.ResetValue();  
        _text.alpha = 1f;
        _text.transform.localScale = _textScale;
    }
}

[Serializable]
public class RandomPositionTextData : BaseDamageTextData
{
    [field: SerializeField] public float RadiusMin { get; private set; } = 0.5f;
    [field: SerializeField] public float RadiusMax { get; private set; } = 1f;

    [field: SerializeField] public float TextDisplayDuration{ get; private set; } = 0.5f;

    [field: SerializeField] public float EndScaleDuration{ get; private set; } = 0.5f;

    [field: SerializeField] public float TextEndAlpha{ get; private set; } = 0.2f;
    [field: SerializeField] public float AlphaDuration{ get; private set; } = 0.5f;
}
