using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamageText : BaseDamageText
{
    [SerializeField]
        private NormalTextData _textData;
    
        protected override void Awake()
        {
            base.Awake();
        }
    
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
            Vector3 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            dir.Normalize();
    
            float radius = Random.Range(_textData.RadiusMin, _textData.RadiusMax);
    
            dir *= radius;
    
            _text.transform.localPosition = dir;
            
            Sequence sequence = DOTween.Sequence();
    
            sequence.Append(
                _text.transform.DOLocalMoveY( _textData.TextHeight,_textData.UpDownDuration * 0.5f)//.SetEase(Ease.InElastic)
            );
            sequence.Append(
                _text.transform.DOLocalMoveY( dir.y,_textData.UpDownDuration * 0.5f)//.SetEase(Ease.OutElastic)
            );
    
            sequence.AppendInterval(_textData.ScaleDelay);
            
            sequence.Join(
                _text.transform.DOScale(Vector3.zero,_textData.ScaleDuration)//.SetEase(Ease.InElastic)
            );
            
            sequence.Join(
                _text.DOFade(_textData.TextEndAlpha, _textData.AlphaDuration)
            );
            
    
            sequence.AppendCallback(ResetText);
    
            return this;
        }
    
        protected override void ResetValue()
        {
            Debug.Log(_text.alpha);
            base.ResetValue();
            _text.color = _textData.DefaultColor;
            _text.alpha = 1f;
            _text.transform.localScale = Vector3.one * 0.1f;
        }

}
