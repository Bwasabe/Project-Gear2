using System;
using TMPro;
using UnityEngine;

public abstract class BaseDamageText : MonoBehaviour
{
    public abstract BaseDamageTextData TextData{ get; set; }

    protected TMP_Text _text;
    
    protected Vector3 _textScale;

    protected virtual void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();

        _textScale = _text.transform.localScale;
        ResetValue();
    }

    public abstract BaseDamageText ShowText();
    

    public BaseDamageText SetColor(Color color)
    {
        _text.color = color;

        return this;
    }

    public BaseDamageText SetTextSize(float textSize)
    {
        _text.fontSize = textSize;

        return this;
    }

    public BaseDamageText SetPosition(Vector3 pos)
    {
        transform.position = pos;

        return this;
    }

    public virtual BaseDamageText SetText(string text)
    {
        _text.text = text;
        return this;
    }

    protected void ResetText()
    {
        ResetValue();
        PoolManager.Destroy(gameObject);
    }

    protected virtual void ResetValue()
    {
        _text.transform.localScale = _textScale;
        _text.transform.localPosition = Vector3.zero;
        _text.fontSize = TextData.DefaultTextSize;
    }
    
}

[Serializable]
public class BaseDamageTextData
{
    [field: SerializeField]
    public float DefaultTextSize{ get; private set; } = 70f;
    

}
