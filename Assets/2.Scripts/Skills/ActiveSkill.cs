using UnityEngine;
using UnityEngine.UI;

public abstract class ActiveSkill : ScriptableObject
{
    [HideInInspector] public ActiveSkillController skillController;
    public GameObject itemPrefab;
    public Sprite sprite;
    public string skillName;
    public string skillDescription;
    public activeSkillType skillType;
    public float ReloadDuration;
    public int maxActivateNum = 1;
    [HideInInspector] public int currentActivateNum;
    [HideInInspector] public bool isReloading;
    public enum activeSkillType
    {
        Single,
        Infinit
    }
    public abstract void Init();
    public virtual void Activate() {}

    protected virtual void ActivateError(string reason) { skillController.ActivateError(reason); }
}
