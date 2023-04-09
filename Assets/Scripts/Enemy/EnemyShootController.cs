using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] private float delayShoot;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float speedBullet;
    [SerializeField] private float damageBullet;

    private bool _canShoot;
    private float _currentDelay;
    private Transform _target;
    private void Start()
    {
        _target = PlayerController.Instance.transform;
    }
    private void Update()
    {
        if (_canShoot) 
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        _currentDelay -= Time.deltaTime;
        if (_currentDelay > 0) return;

        _currentDelay = delayShoot;

        shootPoint.LookAt(_target.position);
        var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, transform);
        bullet.speed = speedBullet;
        bullet.damage = damageBullet;
        Destroy(bullet.gameObject, 5);
    }

    public void SetActiveCanShoot(bool value)
    {
        _canShoot = value;
    }
}
