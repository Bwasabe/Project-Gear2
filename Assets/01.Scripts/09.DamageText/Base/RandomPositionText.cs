using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPositionText : BaseDamageText
{
    [SerializeField] private RandomPositionTextData _textData;

    public override BaseDamageTextData TextData
    {
        get { return _textData; }
        set { _textData = value as RandomPositionTextData; }
    }

    public override BaseDamageText ShowText()
    {
        Sequence sequence = DOTween.Sequence();

        Vector3 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        dir.Normalize();

        float radius = Random.Range(_textData.RadiusMin, _textData.RadiusMax);

        dir *= radius;

        _text.transform.localPosition = dir;

        Vector3 originScale = Vector3.one * 0.1f;

        _text.transform.localScale = _textData.TextScale;

        sequence.Append(
            _text.transform.DOScale(originScale, _textData.AnimationDuration)
        );

        sequence.AppendInterval(_textData.TextAlphaDelay);

        sequence.Append(
            _text.DOFade(_textData.TextEndAlpha, _textData.AnimationDuration - _textData.TextAlphaDelay)
        );

        sequence.AppendCallback(ResetText);

        return this;
    }

    protected override void ResetValue()
    {
        _text.transform.localScale = Vector3.one * 0.1f;
        base.ResetValue();
    }
}

[Serializable]
public class RandomPositionTextData : BaseDamageTextData
{
    [field: SerializeField] public float RadiusMin { get; private set; } = 0.5f;

    [field: SerializeField] public float RadiusMax { get; private set; } = 1f;

    [field: SerializeField] public Vector3 TextScale { get; private set; } = Vector3.one * 0.15f;

    [field: SerializeField] public float AnimationDuration { get; private set; } = 0.5f;

    [field: SerializeField] public float TextEndAlpha { get; private set; } = 0f;

    [field: SerializeField] public float TextAlphaDelay { get; private set; } = 0.2f;
}
