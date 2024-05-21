using UnityEngine;
using MyPetProject;

public class DefaultEnemyController : AIController
{
    [Header("Follow")]
    public AIState FollowState;
    [Header("Attack")]
    public AIState AttackState;
    public float rayDistanceForAttackState;
    [Header("Patrol")]
    public AIState PatrolState;

    private void Start()
    {
        SetState(PatrolState);
    }

    public override void FindNewState()
    {
        Debug.Log(GetCurrentState()+"поиск нового состояния");
        SetState(AttackState);
    }
}
