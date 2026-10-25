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
  private bool isSpawning = false;
  private Queue<SpawnRequest> requests = new Queue<SpawnRequest>(); // I need use Queue because of it`s easy working with first  element what have been added in list.
  private bool defeatEventSent = true;

  public void Init(List<EnemyConfig> enemyConfigs)
  {
    if (enemyConfigs == null) return;
    this.enemyConfigs = enemyConfigs;
  }

  public void SpawnEnemy(EnemyConfig config)
  {

    EnemyBootstrap enemyBootstrap = Instantiate(config.enemyPrefab, transform.position, Quaternion.identity);
    enemyBootstrap.Init(config, sealComponent);
    spawnedEnemies.Add(enemyBootstrap);
    enemyBootstrap.GetComponent<EnemyHealth>().OnDead += HandleDeathEnemy;
  }
  public void StartSpawnCoroutine(float delay, int maxEnemy)
  {

    requests.Enqueue(new SpawnRequest(Mathf.Max(0f, delay), maxEnemy));
    defeatEventSent = false;
    if (isSpawning) return;
    isSpawning = true;
    StartCoroutine(ProcessRequests());
  }
  IEnumerator ProcessRequests()
  {
    while (requests.Count > 0)
    {
      SpawnRequest request = requests.Dequeue(); // Take the last element ( what have been added  first)
      for (int i = 0; i < request.count; i++)
      {
        EnemyConfig config =
            enemyConfigs[UnityEngine.Random.Range(
                0, enemyConfigs.Count)];

        SpawnEnemy(config);


        if (i < request.count - 1)
          yield return new WaitForSeconds(request.delay);
      }
    }
    isSpawning = false;
    CheckEnemiesDefeated();

  }
  private void CheckEnemiesDefeated()
  {
    if (isSpawning ||
        requests.Count > 0 ||
        spawnedEnemies.Count > 0 ||
        defeatEventSent)
    {
      return;
    }

    defeatEventSent = true;
    OnEnemiesDefeated?.Invoke();
  }
  public void HandleDeathEnemy(EnemyHealth enemyHealth)
  {
    EnemyBootstrap enemyBootstrap = enemyHealth.GetComponent<EnemyBootstrap>();
    EnemyStatus enemyStatus = enemyHealth.GetComponent<EnemyStatus>();
    enemyHealth.OnDead -= HandleDeathEnemy;
    spawnedEnemies.Remove(enemyBootstrap);
    CheckEnemiesDefeated();

  }
  private void OnDestroy()
  {
    foreach (EnemyBootstrap enemy in spawnedEnemies)
    {
      if (enemy != null)
      {
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        health.OnDead -= HandleDeathEnemy;
      }

    }

    spawnedEnemies.Clear();
  }


}