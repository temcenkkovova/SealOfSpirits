using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
  public List<EnemyConfig> enemyConfigs;
  public SealComponent sealComponent;
  private List<EnemyBootstrap> spawnedEnemies = new List<EnemyBootstrap>();
  public event Action OnEnemiesDefeated;

  void Start()
  {
    //SpawnEnemy();
  }
  void OnEnable()
  {
    SpawnEnemy();
  }
  public void SpawnEnemy()
  {
    Vector3 lastSpawnPos = transform.position;

    foreach (EnemyConfig config in enemyConfigs)
    {
      Vector3 nextSpawnPos = lastSpawnPos + new Vector3(2f, 0, 2f);
      EnemyBootstrap enemyBootstrap = Instantiate(config.enemyPrefab, nextSpawnPos, Quaternion.identity);
      enemyBootstrap.Init(config, sealComponent);
      spawnedEnemies.Add(enemyBootstrap);
      enemyBootstrap.GetComponent<EnemyHealth>().OnDead += HandleDeathEnemy;
      lastSpawnPos = nextSpawnPos;
    }

  }

  public void HandleDeathEnemy(EnemyHealth enemyHealth)
  {
    EnemyBootstrap enemyBootstrap = enemyHealth.GetComponent<EnemyBootstrap>();
    enemyHealth.OnDead -= HandleDeathEnemy;
    spawnedEnemies.Remove(enemyBootstrap);
    if (spawnedEnemies.Count == 0)
    {
      OnEnemiesDefeated?.Invoke();
    }
  }



}