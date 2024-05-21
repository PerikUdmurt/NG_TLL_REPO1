using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveSkillList : MonoBehaviour
{
    public List <PassiveSkill> skills;

    private void Update()
    {
        foreach (var skill in skills) 
        {
            skill.Run();
        }
    }

    public bool GetSkill(PassiveSkill skill)
    {
        if (skill != null)
        {
            skills.Add(skill);
            skill.skillList = this;
            skill.Init();
            return true;
        }
        return false;
    }
    public void DropSkill(PassiveSkill skill)
    {
        skills.Remove(skill);
        Instantiate(skill.itemPrefab, gameObject.transform.position, Quaternion.identity);
    }
}


