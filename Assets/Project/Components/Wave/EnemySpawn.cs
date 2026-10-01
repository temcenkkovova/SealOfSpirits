using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
  public List<EnemyConfig> enemyConfigs;
  public SealComponent sealComponent;

  void Start()
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

      lastSpawnPos = nextSpawnPos;
    }
  }
}