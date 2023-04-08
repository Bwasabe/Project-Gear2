using System;
using UnityEngine;

[Serializable]
public class TextContainer
{
    public TextType textType;
    
    public BaseDamageText textPrefab;
    
    [field: SerializeReference]
    public BaseDamageTextData textData;
    public TextContainer(BaseDamageText textPrefab, BaseDamageTextData textData, TextType textType)
    {
        this.textPrefab = textPrefab;
        this.textData = textData;
        this.textType = textType;
    }
}
