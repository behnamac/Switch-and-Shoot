using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public RCC_CarControllerV3 CarController { get; private set; }
        public PlayerMoveController PlayerMove { get; private set; }
        public PlayerShootController PlayerShoot { get; private set; }
        public PlayerHealthController PlayerHealth { get; private set; }
        public Rigidbody Rigid { get; private set; }

        private void Awake()
        {
            Instance = this;

            CarController = GetComponent<RCC_CarControllerV3>();
            PlayerMove = GetComponent<PlayerMoveController>();
            PlayerShoot = GetComponent<PlayerShootController>();
            PlayerHealth = GetComponent<PlayerHealthController>();
            Rigid = GetComponent<Rigidbody>();
        }
    }
}
