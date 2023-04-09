using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Spawn;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyHealthController enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxSpawnEnemy;

    private int indexEnemy;

    private void Awake()
    {
        GameManager.onLevelStart += OnLevelStart;
    }
    private void Start()
    {
        UIManager.Instance.SpawnEnemyImage(maxSpawnEnemy);
    }
    private void Update()
    {
        Vector3 targetMove = PlayerController.Instance.transform.position;
        targetMove.x = 0;
        targetMove.y = transform.position.y;
        transform.position = targetMove;
    }

    private void OnDestroy()
    {
        GameManager.onLevelStart -= OnLevelStart;
    }
    public void SpawnEnemy() 
    {
        UIManager.Instance.UpdateEnemyImage(indexEnemy);
        if (indexEnemy >= maxSpawnEnemy) 
        {
            SpawnPlaceController.Instance.Finish = true;
            return;
        }

        Transform spawnPoint = GetPoint();
        var enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.EnemySpawn = this;
        enemy.GetComponent<EnemyMoveController>().SpawnPoint = spawnPoint;
        indexEnemy++;
    }

    private Transform GetPoint() 
    {
        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }

    private void OnLevelStart() 
    {
        Invoke(nameof(SpawnEnemy), 5);
    }
}
