using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawn
{
    public class SpawnPlaceController : MonoBehaviour
    {
        public static SpawnPlaceController Instance;

        [SerializeField] private Transform[] places;
        [SerializeField] private Transform placeFinish;

        private Transform _oldPlace;
        public bool Finish { get; set; }
        private void Awake()
        {
            Instance = this;
        }
        public void Spawner(Vector3 spawnPoint)
        {
            if (_oldPlace)
                Destroy(_oldPlace.gameObject, 20);

            int randomIndex = Random.Range(0, places.Length);
            Transform place = places[randomIndex];
            if (Finish)
                place = placeFinish;
            _oldPlace = Instantiate(place, spawnPoint, Quaternion.identity);
        }
    }
}
