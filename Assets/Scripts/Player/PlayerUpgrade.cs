using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerUpgrade : MonoBehaviour
    {
        public static PlayerUpgrade Instance;
        private RCC_CarControllerV3 carController;
        private PlayerShootController PlayerShoot;
        private void Awake()
        {
            Instance = this;

            TryGetComponent(out carController);
            TryGetComponent(out PlayerShoot);
        }
        public IEnumerator UpgradeSpeed(float addPower)
        {
            carController.maxspeed += addPower;
            yield return null;
        }
        public IEnumerator UpgradeArmor(float addPower)
        {
            yield return null;
        }
        public IEnumerator UpgradeDamage(float addPower)
        {
            PlayerShoot.damageBullet += addPower;
            yield return null;
        }
    }
}
