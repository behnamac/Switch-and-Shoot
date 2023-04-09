using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private float targetDistanceForward;
    [SerializeField] private float targetDistanceBack;
    [SerializeField, Range(0, 1)] private float speed;

    private PlayerController _playerController;
    private Rigidbody _rigid;
    private Rigidbody _playerRigid;
    private EnemyHealthController healthController;
    private EnemyShootController enemyShoot;
    private bool _collision;

    public Transform SpawnPoint { get; set; }
    private void Awake()
    {
        TryGetComponent(out _rigid);
        TryGetComponent(out healthController);
        TryGetComponent(out enemyShoot);
    }

    private void Start()
    {
        _playerController = PlayerController.Instance;
        _playerRigid = _playerController.Rigid;
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>()) 
        {
            _collision = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            _collision = false;
        }
    }

    private void Move() 
    {
        if (healthController.ActiveDead) return;
        Vector3 targetVelocity = _rigid.velocity;
        float valueMove = _playerRigid.velocity.magnitude;

        targetVelocity.z = _playerRigid.velocity.z + (valueMove * speed);
        if (CheckDistanceForward() || _collision)
            targetVelocity.z = _playerRigid.velocity.z;

        enemyShoot.SetActiveCanShoot(CheckDistanceForward());

        if (!CheckRotateCar() || healthController.ActiveDead)
            targetVelocity.z = 0;

        if (CheckDistanceBack())
        {
            _rigid.isKinematic = true;
            transform.position = SpawnPoint.position;
            transform.eulerAngles = Vector3.zero;
            _rigid.isKinematic = false;
        }

        _rigid.velocity = targetVelocity;
    }

    private bool CheckDistanceForward() 
    {
        if (transform.position.z < _playerController.transform.position.z + targetDistanceForward)
            return false;

        return true;
    }
    private bool CheckDistanceBack()
    {
        if (transform.position.z > _playerController.transform.position.z - targetDistanceBack)
            return false;

        return true;
    }
    private bool CheckRotateCar() 
    {
        if (transform.eulerAngles.z > 80 && transform.eulerAngles.z < 360 - 80) 
            return false;

        return true;
    }
}
