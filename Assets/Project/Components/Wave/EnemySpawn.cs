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
    foreach (EnemyConfig config in enemyConfigs)
    {
      EnemyBootstrap enemyBootstrap = Instantiate(config.enemyPrefab, sealComponent.transform);
      enemyBootstrap.Init(config, sealComponent);
    }
  }
}