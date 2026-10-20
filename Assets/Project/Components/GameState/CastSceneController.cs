using UnityEngine;

public class CastSceneController : MonoBehaviour
{

  public GameManager gameManager;

  public ChaosComponent chaosComponent;
  public WaveTimer waveTimer;
  public SealComponent sealComponent;

  void Start()
  {
    if (sealComponent == null || chaosComponent == null || waveTimer == null) return;
    sealComponent.OnSealProgressCompleted += StartVictoryScene;
    chaosComponent.OnFailed += HandleChaosGameOver;
    waveTimer.OnTimerFinished += HandleFinishTimerGameOver;
    Debug.Log("t");
  }


  public void StartVictoryScene()
  {
    Debug.Log("Here will be victory scene , after victory scene will be menu panel");
    GamePhaseController.Instance.ChangeGamePhase(GamePhase.Victory);

  }

  public void HandleChaosGameOver()
  {
    Debug.Log("Here will be chaos game over  scene , after this  scene will be game over panel");
    GameStateController.Instance.ChangeGameState(GameState.GameOver);
  }
  public void HandleFinishTimerGameOver()
  {
    Debug.Log("Here will be timer game over scene , after this scene will be game over panel");
    GameStateController.Instance.ChangeGameState(GameState.GameOver);
  }
  void OnDisable()
  {
    if (sealComponent == null || chaosComponent == null || waveTimer == null) return;
    sealComponent.OnSealProgressCompleted -= StartVictoryScene;
    chaosComponent.OnFailed -= HandleChaosGameOver;
    waveTimer.OnTimerFinished -= HandleFinishTimerGameOver;
  }
}