using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
  private List<EnemyConfig> enemyConfigs;
  public SealComponent sealComponent;
  private List<EnemyBootstrap> spawnedEnemies = new List<EnemyBootstrap>();
  public event Action OnEnemiesDefeated;

  //private Vector3 lastSpawnPos;

  void Start()
  {
    //lastSpawnPos = transform.position;
  }

  public void Init(List<EnemyConfig> enemyConfigs)
  {
    this.enemyConfigs = enemyConfigs;
  }
  // void OnEnable()
  // {
  //   SpawnEnemy();
  // }
  public void SpawnEnemy(EnemyConfig config)
  {
    //Vector3 nextSpawnPos = lastSpawnPos + new Vector3(2f, 0, 2f);
    EnemyBootstrap enemyBootstrap = Instantiate(config.enemyPrefab, transform.position, Quaternion.identity);
    enemyBootstrap.Init(config, sealComponent);
    spawnedEnemies.Add(enemyBootstrap);
    enemyBootstrap.GetComponent<EnemyHealth>().OnDead += HandleDeathEnemy;
    //lastSpawnPos = nextSpawnPos;
  }
  public void StartSpawnCoroutine(int delay)
  {
    StartCoroutine(spawnDelay(delay));
  }
  IEnumerator spawnDelay(int delay)
  {

    foreach (EnemyConfig config in enemyConfigs)
    {
      SpawnEnemy(config);
      yield return new WaitForSeconds(delay);
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