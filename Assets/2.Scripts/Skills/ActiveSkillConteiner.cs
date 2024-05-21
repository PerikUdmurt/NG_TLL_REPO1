using UnityEngine;
[ RequireComponent(typeof(Usable))]
public class ActiveSkillConteiner : MonoBehaviour
{
    public ActiveSkill activeSkill;
    private Usable _usable;

    private void Awake()
    {
        _usable = GetComponent<Usable>();
    }

    private void OnEnable()
    {
        _usable.OnUse.AddListener(SetSkill);
    }

    public void SetSkill(UserController user)
    {
        if (user.gameObject.GetComponentInParent<ActiveSkillController>())
        {
            ActiveSkillController asc = user.gameObject.GetComponentInParent<ActiveSkillController>();
            bool canSetSkill = asc.SetActiveSkill(activeSkill);
            if (canSetSkill) { Destroy(gameObject); }
        }
        else Debug.Log("Отсутствует UserController");
    }
}
