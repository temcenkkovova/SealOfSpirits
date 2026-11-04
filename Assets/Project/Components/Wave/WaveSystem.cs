
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
  public List<FaultComponent> enemySpawnFaults = new List<FaultComponent>();
  public WaveTimer waveTimer;
  public TutorialManager tutorialManager;
  public float spawnTimer;
  private float currentWaveTimer;
  private int intervalDuration = 10;
  private int requiredFaults;
  private int enemiesPerInterval;
  private bool intervalStarted;
  private int intervalBudget;
  private int nextSpawnStage;
  private int nextFaultIndex;
  public event Action<int> OnRequiredFaultsChanged;


  private List<FaultComponent> activeFaults = new List<FaultComponent>();
  void Start()
  {

    if (GamePhaseController.Instance.IsTutorialCompletedOnce)
    {

    }
    else
    {
      // if (tutorialManager != null)
      //tutorialManager.OnFinishTutorial += StartWaveAfterTutorial;
    }

    if (waveTimer != null)
      waveTimer.OnTimerChanged += HandleChangeWaveTime;
  }

  void Update()
  {
    if (!IsPlayingGame()) return;
    UpdateDifficulty();
    ActivateRequiredFaults();
    if (activeFaults.Count == 0)
      return;

    if (!intervalStarted)
    {
      BeginInterval();
    }
    else
    {
      spawnTimer += Time.deltaTime;
      ExecuteDueStages();

      if (spawnTimer >= intervalDuration)
      {
        BeginInterval();
      }
    }
    ExecuteDueStages();
  }

  public void UpdateDifficulty()
  {
    if (currentWaveTimer < 30f)
    {
      requiredFaults = 2;
      enemiesPerInterval = 6;
    }
    else if (currentWaveTimer < 60f)
    {
      requiredFaults = 3;
      enemiesPerInterval = 7;
    }
    else if (currentWaveTimer < 120f)
    {
      requiredFaults = 4;
      enemiesPerInterval = 8;
    }
    else if (currentWaveTimer < 180f)
    {
      requiredFaults = 5;
      enemiesPerInterval = 9;
    }
    else if (currentWaveTimer < 240f)
    {
      requiredFaults = 6;
      enemiesPerInterval = 10;
    }
    else if (currentWaveTimer < 360f)
    {
      requiredFaults = 7;
      enemiesPerInterval = 11;
    }
    else if (currentWaveTimer < 480f)
    {
      requiredFaults = 8;
      enemiesPerInterval = 12;
    }
    else
    {
      requiredFaults = 9;
      enemiesPerInterval = 13;
    }
    OnRequiredFaultsChanged?.Invoke(requiredFaults);
  }
  public void ActivateRequiredFaults()
  {
    activeFaults.RemoveAll(fault => fault == null);
    foreach (FaultComponent fault in enemySpawnFaults)
    {
      if (activeFaults.Count >= requiredFaults)
        break;

      if (fault == null || activeFaults.Contains(fault))
        continue;

      fault.gameObject.SetActive(true);
      activeFaults.Add(fault);
    }
  }
  private void BeginInterval()
  {
    intervalStarted = true;
    spawnTimer = 0f;
    nextSpawnStage = 0;
    intervalBudget = enemiesPerInterval;
  }
  private void ExecuteDueStages()
  {
    while (nextSpawnStage < 3 &&
           spawnTimer >= nextSpawnStage * 3f)
    {
      int groupSize = GetGroupSize(nextSpawnStage);

      if (groupSize > 0)
        SendGroupToNextFault(groupSize);
      nextSpawnStage++;
    }
  }
  private int GetGroupSize(int stage)
  {
    int baseSize = intervalBudget / 3;
    int remainder = intervalBudget % 3;
    return baseSize + (stage < remainder ? 1 : 0);
  }
  private void SendGroupToNextFault(int groupSize)
  {
    if (activeFaults.Count == 0)
      return;
    nextFaultIndex %= activeFaults.Count;
    FaultComponent fault = activeFaults[nextFaultIndex];
    nextFaultIndex = (nextFaultIndex + 1) % activeFaults.Count;

    fault.InitMaxNumberEnemy(groupSize);
  }

  public void HandleChangeWaveTime(float time)
  {
    currentWaveTimer = time;
  }

  public bool IsPlayingGame()
  {
    return GamePhaseController.Instance.CurrentGamePhase == GamePhase.Ritual && GameStateController.Instance.CurrentGameState == GameState.Gameplay;

  }
  void OnDisable()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged -= HandleChangeWaveTime;


  }
}