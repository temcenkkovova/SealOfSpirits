
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
  private float waveDuration = 300;
  private float waveTimer;
  public event Action<float> OnTimerChanged;
  public List<EnemySpawn> enemySpawnFaults;
  private int maxCurrentFaults = 0;

  private bool gameState = true; // It`s a temporary condition to start spawning enemy; It needs to make introducing history and show how to learn skills
  void Start()
  {
    CheckActiveFaultStatus();
  }
  void Update()
  {
    waveTimer += Time.deltaTime;
    OnTimerChanged?.Invoke(waveTimer);

    if (waveTimer >= waveDuration)
    {
      Debug.Log("Finish game");
    }
    if (waveTimer >= 30) maxCurrentFaults = 2;
    if (waveTimer >= 90) maxCurrentFaults = 3;
    if (waveTimer >= 120) maxCurrentFaults = 4;
    if (waveTimer >= 160) maxCurrentFaults = 6;
    if (waveTimer >= 230) maxCurrentFaults = 8;
    if (waveTimer >= 260) maxCurrentFaults = 9;
    CheckActiveFaultStatus();
  }

  public void CheckActiveFaultStatus()
  {
    if (!gameState) return;
    for (int i = 0; i <= maxCurrentFaults; i++)
    {
      enemySpawnFaults[i].gameObject.SetActive(true);
    }
  }

}