
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
  public List<EnemySpawn> enemySpawnFaults;
  private int maxCurrentFaults = 0;
  public WaveTimer waveTimer;
  public TutorialManager tutorialManager;
  void Start()
  {
    if (GamePhaseController.Instance.IsTutorialCompletedOnce)
    {
      CheckActiveFaultStatus(1);
    }
    else
    {
      if (tutorialManager != null)
        tutorialManager.OnFinishTutorial += StartWaveAfterTutorial;
    }

    if (waveTimer == null) return;
    waveTimer.OnTimerChanged += CheckActiveFaultStatus;
  }
  public void StartWaveAfterTutorial()
  {
    CheckActiveFaultStatus(1);
  }
  public void CheckActiveFaultStatus(float time)
  {
    if (!IsPlayingGame()) return;

    if (time >= 30) maxCurrentFaults = 2;
    if (time >= 90) maxCurrentFaults = 3;
    if (time >= 120) maxCurrentFaults = 4;
    if (time >= 160) maxCurrentFaults = 6;
    if (time >= 230) maxCurrentFaults = 8;
    if (time >= 260) maxCurrentFaults = 9;

    for (int i = 0; i <= maxCurrentFaults; i++)
    {
      enemySpawnFaults[i].gameObject.SetActive(true);
    }
  }

  public bool IsPlayingGame()
  {
    return GamePhaseController.Instance.CurrentGamePhase == GamePhase.Ritual && GameStateController.Instance.CurrentGameState == GameState.Gameplay;

  }
  void OnDisable()
  {
    if (waveTimer == null || tutorialManager == null) return;
    waveTimer.OnTimerChanged -= CheckActiveFaultStatus;
    tutorialManager.OnFinishTutorial -= StartWaveAfterTutorial;
  }
}