using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillConteiner : MonoBehaviour
{
    public PassiveSkill passiveSkill;
    private Usable _usable;

    private void OnEnable()
    {
        _usable = GetComponent<Usable>();
        _usable.OnUse.AddListener(SetSkill);
    }

    public void SetSkill(UserController user)
    {
        if (user.gameObject.GetComponentInParent<PassiveSkillList>())
        {
            PassiveSkillList psl = user.gameObject.GetComponentInParent<PassiveSkillList>();
            bool canGetSkill = psl.GetSkill(passiveSkill);
            if (canGetSkill) { Destroy(gameObject); }
        }
        else Debug.Log("Отсутствует UserController");
    }
}
