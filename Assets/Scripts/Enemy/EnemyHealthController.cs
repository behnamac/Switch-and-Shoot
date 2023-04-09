using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private float _currentHealth;
    private Rigidbody _rigid;
    public EnemySpawnController EnemySpawn { get; set; }
    public bool ActiveDead { get; set; }

    private void Awake()
    {
        _currentHealth = maxHealth;

        TryGetComponent(out _rigid);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBulletController bullet)) 
        {
            TakeDamage(bullet.damage);
            Destroy(bullet.gameObject);
        }
    }

    public void TakeDamage(float value) 
    {
        _currentHealth -= value;

        if (_currentHealth <= 0)
            Dead();
    }
    private void Dead() 
    {
        ActiveDead = true;
        _rigid.velocity = new Vector3(Random.Range(-10, 10), 10, 0);
        float[] angular = { 10, -10 };
        int indexAngle = Random.Range(0, angular.Length);
        _rigid.angularVelocity = new Vector3(0, 0, angular[indexAngle]);
        EnemySpawn.Invoke(nameof(EnemySpawn.SpawnEnemy), 5);

        Destroy(gameObject, 3);
    }
}
