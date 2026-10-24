using System;
using System.Collections.Generic;
using UnityEngine;

public class SealComponent : MonoBehaviour
{
  public float currentSealProgress { get; private set; } = 0;
  public float requiredSealProgress { get; private set; } = 100f; // 100
  public event Action<float> OnCurrentSealProgressChanged;
  public event Action OnSealProgressCompleted;
  public GameObject sealWarningVisual;
  public bool IsInDangerZone { get; private set; }
  public ChaosComponent chaosComponent;

  private List<EnemyBootstrap> enemyDangerZone = new List<EnemyBootstrap>();


  void Update()
  {
    enemyDangerZone.RemoveAll(enemy => enemy == null);
    if (enemyDangerZone.Count == 0)
    {

      IsInDangerZone = false;
      sealWarningVisual.SetActive(IsInDangerZone);
    }
  }
  public void AddProgressAmount(float amount)
  {
    currentSealProgress += amount;
    if (currentSealProgress >= requiredSealProgress)
    {
      OnSealProgressCompleted?.Invoke();
    }

    OnCurrentSealProgressChanged?.Invoke(amount);
  }

  void OnTriggerEnter(Collider other)
  {
    if (other == null) return;
    if (other.tag != "Enemy") return;


    EnemyBootstrap enemyBootstrap = other.GetComponent<EnemyBootstrap>();
    if (enemyBootstrap == null) return;

    if (!enemyDangerZone.Contains(enemyBootstrap))
    {
      enemyDangerZone.Add(enemyBootstrap);
      chaosComponent.IncreaseChaos(enemyBootstrap.enemyConfig.chaosAmount);
      EnemyHealth enemyHealth = enemyBootstrap.GetComponent<EnemyHealth>();
      enemyHealth.OnDead += HandleEnemyExit;
    }
    UpdateDangerZone();

  }

  public void HandleEnemyExit(EnemyHealth enemyHealth)
  {
    enemyHealth.OnDead -= HandleEnemyExit;
    EnemyBootstrap enemyBootstrap = enemyHealth.GetComponent<EnemyBootstrap>();
    if (enemyBootstrap == null) return;
    float chaosAmount = enemyBootstrap.enemyConfig.chaosAmount;
    if (enemyDangerZone.Remove(enemyBootstrap))
    {

      chaosComponent.DecreaseChaos(
         chaosAmount);
    }
    UpdateDangerZone();
  }
  void OnTriggerExit(Collider other)
  {
    if (other == null) return;
    if (!other.CompareTag("Enemy")) return;

    EnemyBootstrap enemyBootstrap = other.GetComponent<EnemyBootstrap>();
    if (enemyBootstrap == null) return;

    if (enemyDangerZone.Remove(enemyBootstrap))
    {

      chaosComponent.DecreaseChaos(
          enemyBootstrap.enemyConfig.chaosAmount);
    }

    UpdateDangerZone();
  }
  private void UpdateDangerZone()
  {
    IsInDangerZone = enemyDangerZone.Count > 0;
    sealWarningVisual.SetActive(IsInDangerZone);
  }
}