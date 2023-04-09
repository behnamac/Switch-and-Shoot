using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private float speedShootAim;
        [SerializeField] private float delayShoot;
        [SerializeField] private PlayerBulletController bulletPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float speedBullet;
        public float damageBullet;

        private FixedTouchField _touchField;
        private Transform _shootAim;
        private bool _canShoot;
        private UIManager uIManager;
        private void Start()
        {
            uIManager = UIManager.Instance;
            _shootAim = uIManager.shooterAim;
            _touchField = uIManager.TouchField;
        }
        private void Update()
        {
            if (_canShoot)
                MoveShootAim();
        }

        private void MoveShootAim()
        {
            Vector2 axis = _touchField.TouchDist;
            uIManager.UpdateShooterAim(axis * speedShootAim * Time.deltaTime);
        }
        private void Shoot() 
        {
            bool hit;
            RaycastHit raycastHit;
            Vector3 farPos;
            CheckRaycast(out hit, out raycastHit, out farPos);
            if (hit)
                shootPoint.LookAt(raycastHit.point);
            else
                shootPoint.LookAt(farPos);
            var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, transform);
            bullet.speed = speedBullet;
            bullet.damage = damageBullet;
            Destroy(bullet.gameObject, 5);
        }

        private void CheckRaycast(out bool findObject, out RaycastHit raycastHit, out Vector3 farPos)
        {
            Camera cam = Camera.main;
            Vector3 aimPosFar = new Vector3(_shootAim.position.x, _shootAim.position.y, cam.farClipPlane);
            Vector3 aimPosNear = new Vector3(_shootAim.position.x, _shootAim.position.y, cam.nearClipPlane);
            Vector3 aimPosF = cam.ScreenToWorldPoint(aimPosFar);
            Vector3 aimPosN = cam.ScreenToWorldPoint(aimPosNear);

            Ray ray = new Ray(aimPosN, aimPosF - aimPosN);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            findObject = hasHit;
            raycastHit = hit;
            farPos = aimPosF;
        }

        public void SetActiveCanShoot(bool value) 
        {
            _canShoot = value;
            if (_canShoot)
                InvokeRepeating(nameof(Shoot), delayShoot, delayShoot);
            else
                CancelInvoke(nameof(Shoot));
        }
    }
}
