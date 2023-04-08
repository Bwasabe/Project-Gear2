using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoSingleton<DamageTextManager>
{

    [SerializeField] private List<TextContainer> _textContainer;

    public List<TextContainer> TextContainer => _textContainer;

    private readonly Dictionary<TextType, BaseDamageText> _textDict = new();


    private void Start()
    {
        InitText();
    }


    public void AddTextContainer(TextContainer container)
    {
        _textContainer.Add(container);
    }

    public void ResetTextContainer()
    {
        _textContainer.Clear();
    }

    private void InitText()
    {
        foreach (TextContainer container in _textContainer)
        {
            if (!_textDict.TryAdd(container.textType, container.textPrefab))
            {
                throw new SystemException("TextType is overlap in dictionary");
            }
        }
    }

    public BaseDamageText GetDamageText(TextType type)
    {
        if (_textDict.TryGetValue(type, out BaseDamageText textPrefab))
        {
            BaseDamageText damageText = PoolManager.Instantiate(textPrefab.gameObject).GetComponent<BaseDamageText>();
            return damageText;
        }
        else
            throw new SystemException("Type is Null in Dict");
    }

    public BaseDamageText GetDamageText(TextType type, Color color, Vector3 position, string text)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetColor(color);
        damageText.SetPosition(position);
        damageText.SetText(text);

        return damageText;
    }

    public BaseDamageText GetDamageText(TextType type, Color color, string text)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetColor(color);
        damageText.SetText(text);

        return damageText;
    }

    public BaseDamageText GetDamageText(TextType type, Vector3 position, string text)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetPosition(position);
        damageText.SetText(text);

        return damageText;
    }

    public BaseDamageText GetDamageText(TextType type, Color color, Vector3 position)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetColor(color);
        damageText.SetPosition(position);

        return damageText;
    }

    public BaseDamageText GetDamageText(TextType type, Color color)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetColor(color);

        return damageText;
    }

    public BaseDamageText GetDamageText(TextType type, Vector3 position)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetPosition(position);

        return damageText;
    }

    public BaseDamageText GetDamageText(TextType type, string text)
    {
        BaseDamageText damageText = GetDamageText(type);
        damageText.SetText(text);

        return damageText;
    }

}