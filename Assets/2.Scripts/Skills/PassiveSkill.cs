using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class PassiveSkill : ScriptableObject
{
    [HideInInspector]public PassiveSkillList skillList;
    public GameObject itemPrefab;
    public Sprite skillSprite;
    public string skillName;
    public string skillDescription;
    public List<PassiveSkill> includeSkillsToList;
    public List<PassiveSkill> excludeSkillsToList;

    public abstract void Init();

    public virtual void Run() { }
}
