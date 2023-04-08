using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DamageTextManager))]
public class DamageTextEditor : Editor
{
    private const string DAMAGETEXT_FOLDER_PATH = "Assets/03.Prefabs/DamageText";

    private DamageTextManager _damageTextManager;
    public override void OnInspectorGUI()
    {
        _damageTextManager = (DamageTextManager)target;
        GUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Refresh"))
            {
                _damageTextManager.ResetTextContainer();
                List<BaseDamageText> damageTexts = GetAllDamageTextInProject();
                for (int i = 0; i < damageTexts.Count; ++i)
                {
                    int index = i;
                    _damageTextManager.AddTextContainer(new TextContainer(damageTexts[i], damageTexts[i].TextData, (TextType)index)); 
                }
                RefreshTextContainer();            
            }
            if(GUILayout.Button("Apply"))
                ApplyTextContainer();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
    
    
    private void RefreshTextContainer()
    {
        foreach (TextContainer container in _damageTextManager.TextContainer)
        {
            BaseDamageText damageText = container.textPrefab.GetComponent<BaseDamageText>();
            container.textData = damageText.TextData;
            EditorUtility.SetDirty(damageText);
        }
    }

    private void ApplyTextContainer()
    {
        foreach (TextContainer container in _damageTextManager.TextContainer)
        {
            BaseDamageText damageText = container.textPrefab.GetComponent<BaseDamageText>();
            damageText.TextData = container.textData;
            EditorUtility.SetDirty(damageText);
        }
    }


    private List<BaseDamageText> GetAllDamageTextInProject()
    {
        var textList = AssetDatabase.FindAssets("t:GameObject" , new []{DAMAGETEXT_FOLDER_PATH}).ToList()
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
            .Select(x => x.GetComponent<BaseDamageText>())
            .ToList();

        return textList;
    }
}
