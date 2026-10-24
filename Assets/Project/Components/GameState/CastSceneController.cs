using System;
using UnityEngine;

public class CastSceneController : MonoBehaviour
{

  public GameManager gameManager;

  public ChaosComponent chaosComponent;
  public WaveTimer waveTimer;
  public SealComponent sealComponent;
  public event Action<string> OnShowDefeatedInfo;

  void Start()
  {
    if (sealComponent == null || chaosComponent == null || waveTimer == null) return;
    sealComponent.OnSealProgressCompleted += StartVictoryScene;
    chaosComponent.OnFailed += HandleChaosGameOver;
    waveTimer.OnTimerFinished += HandleFinishTimerGameOver;

  }


  public void StartVictoryScene()
  {
    Debug.Log("Here will be victory scene , after victory scene will be menu panel");
    GamePhaseController.Instance.ChangeGamePhase(GamePhase.Victory);
    GameStateController.Instance.ChangeGameState(GameState.Finished);

  }

  public void HandleChaosGameOver()
  {

    GamePhaseController.Instance.ChangeGamePhase(GamePhase.Defeat);
    OnShowDefeatedInfo?.Invoke($"The seal was destroyed , you have got {sealComponent.currentSealProgress} %");
    GameStateController.Instance.ChangeGameState(GameState.Finished);
  }
  public void HandleFinishTimerGameOver()
  {

    GamePhaseController.Instance.ChangeGamePhase(GamePhase.Defeat);
    OnShowDefeatedInfo?.Invoke($"Time was run out");
    GameStateController.Instance.ChangeGameState(GameState.Finished);
  }
  void OnDisable()
  {
    if (sealComponent == null || chaosComponent == null || waveTimer == null) return;
    sealComponent.OnSealProgressCompleted -= StartVictoryScene;
    chaosComponent.OnFailed -= HandleChaosGameOver;
    waveTimer.OnTimerFinished -= HandleFinishTimerGameOver;
  }
}