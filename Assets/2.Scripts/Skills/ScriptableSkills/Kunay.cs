using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableSkills/Active/Kunay")]
public class Kunay : ActiveSkill
{
    public override void Init(){}
    public override void Activate()
    {
        Debug.Log("Брошен кунай");
    }
}
