using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Spawn
{
    public class SpawnPlaceChecker : MonoBehaviour
    {
        [SerializeField] private Transform spawnNextPlacePoint;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                SpawnPlaceController.Instance.Spawner(spawnNextPlacePoint.position);
            }
        }
    }
}
