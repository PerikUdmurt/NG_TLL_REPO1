using UnityEngine;
public class DefaultEnemyAttack : AIState
{
    private Collider2D _col;
    public LayerMask targetLayer;
    public Transform targetTransform;
    public float moveToTargetDistance;
    public float attackDistance;
    public void Awake()
    {
        _col = GetComponent<Collider2D>();
    }
    public override void Play()
    {
        AI.fastAttack = false;

        bool targetCheck = Physics2D.Raycast(_col.bounds.center, Vector2.right * CheckCurrentDirection(), moveToTargetDistance , targetLayer);
        bool attackCheck = Physics2D.Raycast(_col.bounds.center, Vector2.right * CheckCurrentDirection(), attackDistance, targetLayer);

        if (!targetCheck) { End(); }

        if (attackCheck) { AI.fastAttack = true;}
    }
}
