using System;
using System.Collections.Generic;
using UnityEngine;

public class SealComponent : MonoBehaviour
{
  public float currentSealProgress { get; private set; } = 0;
  private float requiredSealProgress = 100f;
  public event Action<float> OnCurrentSealProgressChanged;
  public event Action OnSealProgressCompleted;
  public GameObject sealWarningVisual;
  public bool IsInDangerZone { get; private set; }

  private List<EnemyBootstrap> enemyDangerZone = new List<EnemyBootstrap>();


  void Update()
  {
    enemyDangerZone.RemoveAll(enemy => enemy == null);
    if (enemyDangerZone.Count == 0)
    {
      Debug.Log("Zero");
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

    if (other.tag != "Enemy") return;


    EnemyBootstrap enemyBootstrap = other.GetComponent<EnemyBootstrap>();
    if (enemyBootstrap == null) return;

    if (!enemyDangerZone.Contains(enemyBootstrap))
    {
      enemyDangerZone.Add(enemyBootstrap);
    }
    UpdateDangerZone();

  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Enemy")
    {
      EnemyBootstrap enemyBootstrap = other.GetComponent<EnemyBootstrap>();
      if (enemyBootstrap == null) return;

      foreach (EnemyBootstrap enemy in enemyDangerZone)
      {
        if (enemyBootstrap == enemy)
          enemyDangerZone.Remove(enemy);
      }
      UpdateDangerZone();
    }
  }
  private void UpdateDangerZone()
  {
    IsInDangerZone = enemyDangerZone.Count > 0;
    sealWarningVisual.SetActive(IsInDangerZone);
  }
}