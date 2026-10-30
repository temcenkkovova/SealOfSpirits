using System;
using System.Collections;
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
  private Coroutine resetChaosCoroutine;

  private bool isClearZone = false;
  private List<EnemyBootstrap> enemyDangerZone = new List<EnemyBootstrap>();


  void Update()
  {

    enemyDangerZone.RemoveAll(enemy => enemy == null);
    UpdateDangerZone();
    sealWarningVisual.SetActive(IsInDangerZone);

    if (IsInDangerZone)
    {

      if (resetChaosCoroutine != null)
      {
        StopCoroutine(resetChaosCoroutine);
        resetChaosCoroutine = null;
      }

      isClearZone = false;
      return;
    }

    chaosComponent.ResetChaosMultiply();
    if (!isClearZone && resetChaosCoroutine == null)
    {
      resetChaosCoroutine = StartCoroutine(ClearZoneCoroutine());
    }

    if (isClearZone)
    {
      chaosComponent.DecreaseChaosValue(2f * Time.deltaTime);
    }
  }
  public IEnumerator ClearZoneCoroutine()
  {
    yield return new WaitForSeconds(2f);
    isClearZone = true;
    resetChaosCoroutine = null;
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
    EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
    float chaosValue = enemyBootstrap.enemyConfig.chaosAmount;
    if (enemyBootstrap == null || enemyStatus == null) return;

    if (!enemyDangerZone.Contains(enemyBootstrap))
    {
      enemyDangerZone.Add(enemyBootstrap);
      enemyStatus.ChangeSealRadiusStatus(true);
      if (enemyStatus.IsChaosDecreased)
      {
        chaosValue *= 0.5f;
      }
      Debug.Log(chaosValue);
      chaosComponent.IncreaseChaosMultiply(chaosValue);
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

      chaosComponent.DecreaseChaosMultiply(
         chaosAmount);
    }
    UpdateDangerZone();
  }
  void OnTriggerExit(Collider other)
  {
    if (other == null) return;
    if (!other.CompareTag("Enemy")) return;

    EnemyBootstrap enemyBootstrap = other.GetComponent<EnemyBootstrap>();
    EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
    if (enemyBootstrap == null || enemyStatus == null) return;

    if (enemyDangerZone.Remove(enemyBootstrap))
    {
      enemyStatus.ChangeSealRadiusStatus(false);
      chaosComponent.DecreaseChaosMultiply(
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