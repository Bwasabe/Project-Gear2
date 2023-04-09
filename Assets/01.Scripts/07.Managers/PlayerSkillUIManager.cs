using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillUIManager : MonoSingleton<PlayerSkillUIManager>
{

    [SerializeField]
    private PlayerSkillUI[] _skillUIs;


    public void AddSkill(CharacterSkillBase skillBase, int index)
    {
        _skillUIs[index].SetSkillBase(skillBase);
    }
}
