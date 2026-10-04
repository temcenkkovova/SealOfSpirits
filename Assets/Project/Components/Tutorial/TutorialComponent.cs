using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComponent : MonoBehaviour
{
  public List<EnemyConfig> enemyConfigs;

  public SealComponent sealComponent;

  private List<EnemyBootstrap> spawnedEnemies = new List<EnemyBootstrap>();
  private float tutorialStage = 0;
  public EnemySpawn secondFaultTutorialEnemy;
  public event Action OnTutorialFinished;
  public event Action FirstSkillTutorialFinished;
  public event Action SecondSkillTutorialFinished;
  public event Action OnEnemiesDead;

  private bool isSecondGroupDefeated = false;
  private bool isTutorialFinished = false;

  void Update()
  {
    if (isTutorialFinished) return;
    if (tutorialStage >= 3 && spawnedEnemies.Count == 0 && isSecondGroupDefeated)
    {
      OnTutorialFinished?.Invoke();
      GamePhaseController.Instance.HandleFinishTutorial(); // I have to create an bridge between tutorial and Main gameplay;
      isTutorialFinished = true;
    }
  }
  void Start()
  {
    secondFaultTutorialEnemy.gameObject.SetActive(false);
  }
  public void SpawnTutorialEnemy()
  {
    if (tutorialStage > 2) return;
    Vector3 lastSpawnPos = transform.position;

    for (int i = 0; i <= tutorialStage; i++)
    {
      Vector3 nextSpawnPos = lastSpawnPos + new Vector3(2f, 0, 2f);
      EnemyBootstrap enemyBootstrap = Instantiate(enemyConfigs[i].enemyPrefab, nextSpawnPos, Quaternion.identity);
      enemyBootstrap.Init(enemyConfigs[i], sealComponent);
      spawnedEnemies.Add(enemyBootstrap);
      enemyBootstrap.GetComponent<EnemyHealth>().OnDead += HandleTutorialEnemyDeath;
      lastSpawnPos = nextSpawnPos;
    }

    if (tutorialStage == 2)
    {
      secondFaultTutorialEnemy.gameObject.SetActive(true);
      secondFaultTutorialEnemy.OnEnemiesDefeated += HandleDefeatedSecondGroup;

    }
  }
  public void HandleDefeatedSecondGroup()
  {
    isSecondGroupDefeated = true;
  }
  public void HandleTutorialEnemyDeath(EnemyHealth enemyHealth)
  {
    enemyHealth.OnDead -= HandleTutorialEnemyDeath;
    EnemyBootstrap enemyBootstrap = enemyHealth.GetComponent<EnemyBootstrap>();
    spawnedEnemies.Remove(enemyBootstrap);

    if (spawnedEnemies.Count == 0)
    {
      tutorialStage++;
      OnEnemiesDead?.Invoke();
    }
  }
  void OnDisable()
  {
    if (secondFaultTutorialEnemy != null)
      secondFaultTutorialEnemy.OnEnemiesDefeated -= HandleDefeatedSecondGroup;
  }
}