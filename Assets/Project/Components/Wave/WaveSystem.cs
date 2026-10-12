
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
  public List<EnemySpawn> enemySpawnFaults = new List<EnemySpawn>();
  private int maxCurrentFaults = 0;
  public WaveTimer waveTimer;
  public TutorialManager tutorialManager;
  private bool wasChecked = false;
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

    int newMaxNumberFaults = 0;
    if (!IsPlayingGame()) return;

    if (time >= 30)
    {
      newMaxNumberFaults = 2;
      wasChecked = false;
    }

    if (time >= 90)
    {
      newMaxNumberFaults = 3;
      wasChecked = false;
    }

    if (time >= 120)
    {
      newMaxNumberFaults = 4;
      wasChecked = false;
    }
    if (time >= 160)
    {
      newMaxNumberFaults = 6;
      wasChecked = false;
    }
    if (time >= 230)
    {
      newMaxNumberFaults = 8;
      wasChecked = false;
    }
    if (time >= 260)
    {
      newMaxNumberFaults = 9;
      wasChecked = false;
    }


    maxCurrentFaults = newMaxNumberFaults;

    if (wasChecked) return;
    for (int i = 0; i <= maxCurrentFaults; i++)
    {
      enemySpawnFaults[i].gameObject.SetActive(true);
    }
    wasChecked = true;
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