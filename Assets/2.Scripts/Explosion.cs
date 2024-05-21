using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float force;
    private Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 direction = gameObject.transform.position - collision.transform.position;
        collision.attachedRigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public IEnumerator destoyTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
