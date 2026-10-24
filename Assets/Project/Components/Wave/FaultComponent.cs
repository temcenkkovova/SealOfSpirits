using System.Collections;
using UnityEngine;

public class FaultComponent : MonoBehaviour
{
  public FaultConfig faultConfig;
  private EnemySpawn enemySpawn;
  private bool isInitialized;

  void Awake()
  {
    TryInitialize();
  }
  private bool TryInitialize()
  {
    if (isInitialized)
      return true;

    enemySpawn = GetComponent<EnemySpawn>();

    if (enemySpawn == null || faultConfig == null)
    {
      return false;
    }

    enemySpawn.Init(faultConfig.enemyConfigs);
    isInitialized = true;

    return true;
  }

  public void InitMaxNumberEnemy(int maxNumberEnemy)
  {
    if (!isActiveAndEnabled || maxNumberEnemy <= 0)

      return;

    if (!TryInitialize())
      return;

    enemySpawn.StartSpawnCoroutine(
        faultConfig.spawnDelay,
        maxNumberEnemy);
  }
}