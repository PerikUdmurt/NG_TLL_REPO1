using UnityEngine;

public class DefaultEnemyPatrol : AIState
{
    public float patrolRayDistance;
    public LayerMask changeDirectionLayer;
    public LayerMask targetLayer;
    private Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }
    public override void Init()
    {
        
        if (AI.horizontal == 0) AI.horizontal = 1;
    }
    public override void Play()
    {
        bool WallCheck = Physics2D.Raycast(_col.bounds.center, Vector2.right * CheckCurrentDirection(), 1.8f, changeDirectionLayer);
        bool targetCheck = Physics2D.Raycast(_col.bounds.center, Vector2.right * CheckCurrentDirection(), patrolRayDistance, targetLayer);
       
        if (targetCheck)
        {
            End();
        }

        if (WallCheck) ChangeDirection();
        
    }

    private void ChangeDirection()
    {
        AI.horizontal = -AI.horizontal;
    }

    private void OnDrawGizmos()
    {
        if (AI.GetCurrentState() == this)
        {
            Gizmos.DrawLine(_col.bounds.center, _col.bounds.center + new Vector3(CheckCurrentDirection() * patrolRayDistance, 0, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AI.GetCurrentState() == this)
        {
            if (collision.CompareTag("EdgeOfPlatform"))
            {
                ChangeDirection();
            }
        }
    }

}
