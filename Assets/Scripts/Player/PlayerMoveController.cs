using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        private Joystick _joystick;
        private PlayerController _playerController;
        private RCC_CarControllerV3 _carController;
        private Rigidbody _rigid;

        private Vector3 _refVelocity;
        private void Awake()
        {
            GameManager.onLevelStart += OnLevelStart;
            GameManager.onLevelCompelet += OnLevelCompelet;
            GameManager.onLevelFail += OnLevelFail;
        }
        private void Start()
        {
            _joystick = UIManager.Instance.Joystick;
            _playerController = PlayerController.Instance;
            _carController = _playerController.CarController;
            _rigid = _playerController.Rigid;
        }
        // Update is called once per frame
        void Update()
        {
            float steer = _joystick.Horizontal;
            _carController.SetSteerInput(steer);
        }

        private void OnDestroy()
        {
            GameManager.onLevelStart -= OnLevelStart;
            GameManager.onLevelCompelet -= OnLevelCompelet;
            GameManager.onLevelFail -= OnLevelFail;
        }

        private IEnumerator SmoothStop() 
        {
            while (_rigid.velocity.magnitude > 0)
            {
                _rigid.velocity = Vector3.SmoothDamp(_rigid.velocity, Vector3.zero, ref _refVelocity, 2);
                yield return new WaitForEndOfFrame();
            }
        }
        private void OnLevelStart() 
        {
            _carController.StartEngine();
            _carController.SetGasInput(1);
        }
        private void OnLevelCompelet() 
        {
            _carController.KillEngine();
            _carController.SetGasInput(0);
            _carController.SetSteerInput(0);
            StartCoroutine(SmoothStop());
        }
        private void OnLevelFail() 
        {
            _carController.KillEngine();
            _carController.SetGasInput(0);
            _carController.SetSteerInput(0);
            StartCoroutine(SmoothStop());
        }
    }
}
