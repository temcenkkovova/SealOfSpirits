using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComponent : MonoBehaviour
{
  public FaultConfig firstFaultConfig;
  public FaultConfig secondFaultConfig;

  public SealComponent sealComponent;

  private List<EnemyBootstrap> spawnedEnemies = new List<EnemyBootstrap>();

  public GameObject secondFaultTutorialEnemy;

  public event Action OnEnemiesDead;


  void Start()
  {
    secondFaultTutorialEnemy.gameObject.SetActive(false);
  }
  public void SpawnTutorialEnemy(int stageNumber)
  {

    Vector3 lastSpawnPos = transform.position;
    Vector3 lastSpawnPosSecondFault = secondFaultTutorialEnemy.transform.position;
    for (int i = 0; i <= stageNumber; i++)
    {
      Vector3 nextSpawnPos = lastSpawnPos + new Vector3(2f, 0, 2f);
      EnemyBootstrap enemyBootstrap = Instantiate(firstFaultConfig.enemyConfigs[i].enemyPrefab, nextSpawnPos, Quaternion.identity);
      enemyBootstrap.Init(firstFaultConfig.enemyConfigs[i], sealComponent);
      spawnedEnemies.Add(enemyBootstrap);
      enemyBootstrap.GetComponent<EnemyHealth>().OnDead += HandleTutorialEnemyDeath;
      lastSpawnPos = nextSpawnPos;
    }

    if (stageNumber == 2)
    {
      secondFaultTutorialEnemy.gameObject.SetActive(true);
      for (int i = 0; i <= stageNumber; i++)
      {
        Vector3 nextSpawnPos = lastSpawnPosSecondFault + new Vector3(2f, 0, 2f);
        EnemyBootstrap enemyBootstrap = Instantiate(secondFaultConfig.enemyConfigs[i].enemyPrefab, nextSpawnPos, Quaternion.identity);
        enemyBootstrap.Init(secondFaultConfig.enemyConfigs[i], sealComponent);
        spawnedEnemies.Add(enemyBootstrap);
        enemyBootstrap.GetComponent<EnemyHealth>().OnDead += HandleTutorialEnemyDeath;
        lastSpawnPosSecondFault = nextSpawnPos;
      }
    }
  }

  public void HandleTutorialEnemyDeath(EnemyHealth enemyHealth)
  {
    enemyHealth.OnDead -= HandleTutorialEnemyDeath;
    EnemyBootstrap enemyBootstrap = enemyHealth.GetComponent<EnemyBootstrap>();
    spawnedEnemies.Remove(enemyBootstrap);

    if (spawnedEnemies.Count == 0)
    {
      OnEnemiesDead?.Invoke();
    }
  }
  void OnDisable()
  {


  }

  public void HandleDestroyTutorialFaults()
  {

    Destroy(secondFaultTutorialEnemy.gameObject);
    Destroy(gameObject);
  }
}