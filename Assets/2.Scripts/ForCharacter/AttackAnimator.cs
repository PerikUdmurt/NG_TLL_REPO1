using UnityEngine;
using MyPetProject;

[RequireComponent (typeof(Attack))]
public class AttackAnimator : MonoBehaviour
{
    private Attack _attack;
    private Animator _animator;
    private void Awake()
    {
        _attack = GetComponent<Attack> ();
        _animator = GetComponent<Animator> ();
    }

    private void OnEnable()
    {
        _attack.FastAttackActivated.AddListener(FastAttackAnimate);
        _attack.SlowAttackActivated.AddListener(SlowAttackAnimate);
    }

    private void Update()
    {
        _attack.SetComboDamageModificator(_animator.GetFloat("FastAttackModificator"));
    }

    public void FastAttackAnimate() { _animator.SetTrigger("FastAttackTrigger"); }
    public void SlowAttackAnimate() { _animator.SetTrigger("SlowAttackTrigger"); }

}
