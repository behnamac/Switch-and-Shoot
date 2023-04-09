using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FinishLine"))
            {
                GameManager.Instance.LevelCompelet();
            }
            if (other.gameObject.CompareTag("Money")) 
            {
                UIManager.Instance.AddCurrency(5);
                Destroy(other.gameObject);
            }
            if (other.TryGetComponent(out EnemyBullet bullet))
            {
                PlayerController.Instance.PlayerHealth.TakeDamage(bullet.damage);
                Destroy(bullet.gameObject);
            }
        }
    }
}
