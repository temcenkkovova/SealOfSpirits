using System.Collections;
using UnityEngine;

public class FaultComponent : MonoBehaviour
{
  public FaultConfig faultConfig;
  private EnemySpawn enemySpawn;
  private int delayStartSpawnEnemies = 2;

  void Awake()
  {
    enemySpawn = GetComponent<EnemySpawn>();
  }

  void Start()
  {
    if (faultConfig == null) return;
    if (enemySpawn == null) return;
    enemySpawn.Init(faultConfig.enemyConfigs);
    enemySpawn.OnEnemiesDefeated += HandleDestroyFault;
    SpawnFaultEnemies();
  }

  public void SpawnFaultEnemies()
  {
    StartCoroutine(delaySpawn());
  }

  IEnumerator delaySpawn()
  {
    yield return new WaitForSeconds(delayStartSpawnEnemies);
    enemySpawn.StartSpawnCoroutine(faultConfig.spawnDelay);

  }
  void OnDisable()
  {
    if (enemySpawn == null) return;
    enemySpawn.OnEnemiesDefeated -= HandleDestroyFault;
  }

  public void HandleDestroyFault()
  {
    Destroy(gameObject, 1f);
  }
}