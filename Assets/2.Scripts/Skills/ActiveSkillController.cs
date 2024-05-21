using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using MyPetProject;

public class ActiveSkillController : MonoBehaviour, ISkillController
{
    public ActiveSkill firstActiveSkill;
    public ActiveSkill secondActiveSkill;
    public FrameInput frameInput { get; set; }
    public UnityEvent FirstSkillActivated { get; set; }
    public UnityEvent SecondSkillActivated { get; set; }
    public UnityEvent ActivateFailed { get; set; }
    public UnityEvent SetSkillFailed { get; set; }

    private void Update()
    {
        if (frameInput.FirstSkill)
        {
            ActivateSkill(ref firstActiveSkill);
            FirstSkillActivated?.Invoke();
        }
        if (frameInput.SecondSkill)
        {
            ActivateSkill(ref secondActiveSkill);
            SecondSkillActivated?.Invoke();
        }
    }
    //TODO: Добавить выбор в какой слот будет попадать навык
    public bool SetActiveSkill(ActiveSkill skill)
    {
        if (firstActiveSkill == null) 
        { 
            firstActiveSkill = skill; 
            skill.skillController = this;
            skill.Init(); 
            skill.currentActivateNum = skill.maxActivateNum;
            skill.isReloading = false;
            return true;}
        if (secondActiveSkill == null)
        {
            secondActiveSkill = skill;
            skill.skillController = this;
            skill.Init();
            skill.currentActivateNum = skill.maxActivateNum;
            skill.isReloading = false;
            return true;
        }
        else 
        { 
            SetSkillFailed?.Invoke();
            Debug.Log("Нет доступных слотов под предмет"); 
            return false; 
        }
    }

    public void ActivateSkill(ref ActiveSkill skill)
    {
        if (skill != null)
        {
            if (skill.currentActivateNum > 0)
            {
                skill.currentActivateNum--;
                skill.Activate();
                if (skill.currentActivateNum <= 0 && skill.skillType == ActiveSkill.activeSkillType.Single)
                {
                    skill = null;
                    return;
                }
                if (skill.isReloading) { return; }
                StartCoroutine(ReloadSkill(skill));
            }
            else { ActivateError($"закончились доступные применения " + skill.currentActivateNum); }
        }
        else ActivateError("Слот навыка пуст");
    }

    private float skillReloadCooficient;
    public IEnumerator ReloadSkill(ActiveSkill skill)
    {
        skill.isReloading = true;
        Debug.Log("Началась перезарядка навыка. Доступные применения:" + skill.currentActivateNum);
        yield return new WaitForSeconds(skill.ReloadDuration);
        skill.currentActivateNum++;
        if (skill.currentActivateNum < skill.maxActivateNum) { StartCoroutine(ReloadSkill(skill)); }
        else skill.isReloading = false;
    }

    public void AddSkillReloadCoof(float coof)
    {
        skillReloadCooficient += skillReloadCooficient * coof * 0.01f;
    }

    public void DropSkill(ref ActiveSkill skill)
    {
        if (skill != null)
        {
            Instantiate(skill.itemPrefab, gameObject.transform.position,Quaternion.identity);
            skill = null;
        }
        else Debug.Log("Скилл невозможно сбросить. Отсутствует скилл");
    }
    [ContextMenu("DropAllSkills")]
    private void DropAllSkills()
    {
        DropSkill(ref firstActiveSkill);
        DropSkill(ref secondActiveSkill);
    }
    public void ActivateError(string reason)
    {
        Debug.Log("Ошибка использования навыка. Причина: "+ reason);
        ActivateFailed?.Invoke();
    }
}
public interface ISkillController
{
    public FrameInput frameInput { get; set; }
    public UnityEvent FirstSkillActivated { get; set; }
    public UnityEvent SecondSkillActivated { get; set; }
    public UnityEvent ActivateFailed { get; set; }
}
