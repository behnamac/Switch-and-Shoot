using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerSwicher : MonoBehaviour
    {
        private GameObject driverTouch;
        private GameObject shooterTouch;
        private bool _swich;
        private UIManager _uIManager;
        private PlayerShootController _playerShoot;
        private PlayerController _playerController;
        private void Start()
        {
            _uIManager = UIManager.Instance;
            _playerController = PlayerController.Instance;

            var button = _uIManager.SwichButton;
            button.onClick.AddListener(Swich);

            driverTouch = _uIManager.Joystick.gameObject;
            shooterTouch = _uIManager.TouchField.gameObject;

            _playerShoot = _playerController.PlayerShoot;
        }
        private void Swich()
        {
            _swich = !_swich;
            if (_swich)
                ActiveShooter();
            else
                ActiveDriver();
        }

        private void ActiveDriver()
        {
            driverTouch.SetActive(true);
            shooterTouch.SetActive(false);
            _playerShoot.SetActiveCanShoot(false);
            _uIManager.shooterAim.gameObject.SetActive(false);
            CameraController.Instance.ChangeCameraPos("Normal");
        }
        private void ActiveShooter()
        {
            driverTouch.SetActive(false);
            shooterTouch.SetActive(true);
            _playerShoot.SetActiveCanShoot(true);
            _uIManager.shooterAim.gameObject.SetActive(true);
            CameraController.Instance.ChangeCameraPos("Shoot");
        }
    }
}
