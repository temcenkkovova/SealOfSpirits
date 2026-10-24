using System;
using UnityEngine;
public enum GamePhase
{
  Tutorial, Ritual, Victory, Defeat
}
public class GamePhaseController : MonoBehaviour
{
  public GamePhase CurrentGamePhase { get; private set; }
  public event Action<GamePhase> OnGamePhaseChanged;
  public static GamePhaseController Instance;
  public bool IsTutorialCompletedOnce { get; private set; } = false;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }


  void Start()
  {
    ChangeGamePhase(GamePhase.Tutorial);

  }
  public void ChangeGamePhase(GamePhase newPhase)
  {
    if (newPhase == CurrentGamePhase) return;
    CurrentGamePhase = newPhase;
    OnGamePhaseChanged?.Invoke(CurrentGamePhase);
    // if (CurrentGamePhase == GamePhase.Victory || CurrentGamePhase == GamePhase.Defeat)
    //{
    //GameStateController.Instance.ChangeGameState(GameState.Finished);
    //}
  }

  public void HandleFinishTutorial()
  {
    ChangeGamePhase(GamePhase.Ritual);
    IsTutorialCompletedOnce = true;

  }
}