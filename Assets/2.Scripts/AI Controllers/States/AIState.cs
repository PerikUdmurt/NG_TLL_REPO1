using UnityEngine;
using MyPetProject;

public abstract class AIState : MonoBehaviour
{
    [HideInInspector] public AIController AI;

    public virtual void Init()
    {

    }
    public abstract void Play();

    public void End()
    {
       AI.FindNewState();
    }

    protected float CheckCurrentDirection()
    {
        if (gameObject.transform.localScale.x > 0f) return 1f;
        else return -1f;
    }
}
