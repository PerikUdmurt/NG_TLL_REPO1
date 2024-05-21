using UnityEngine;
using MyPetProject;

[CreateAssetMenu(menuName = "ScriptableSkills/Active/TestBomb")]
public class Bomb : ActiveSkill
{
    public GameObject BombPrefab;
    public float force;
    private MovementController _playerRb;
    private Vector3 _instantiatePosition;
    public Vector3 dropDirection;
    public override void Init()
    {
        _playerRb = skillController.GetComponentInParent<MovementController>();
    }

    public override void Activate()
    {
        Vector3 newDropDirection = new Vector3 (dropDirection.x * _playerRb._currentDirection, dropDirection.y, dropDirection.z); 
        _instantiatePosition = _playerRb.gameObject.transform.position + new Vector3 (0,1f,0);
        GameObject bomb = Instantiate(BombPrefab,_instantiatePosition,Quaternion.identity);
        Rigidbody2D bombRigid = bomb.GetComponent<Rigidbody2D>();
        bombRigid.AddForce(newDropDirection * force, ForceMode2D.Impulse);
    }
}
