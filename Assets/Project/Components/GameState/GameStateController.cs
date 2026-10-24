using System;
using UnityEngine;
public enum GameState { Gameplay, Pause, Dialogue, Finished }

public class GameStateController : MonoBehaviour
{
  public GameState CurrentGameState { get; private set; }

  public event Action<GameState> OnGameStateChanged;
  public static GameStateController Instance;
  public bool IsPause { get; private set; } = false;
  public event Action<bool> OnPauseChanged;

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

    ChangeGameState(GameState.Gameplay);
  }

  public void ChangeGameState(GameState newState)
  {
    if (newState == CurrentGameState) return;
    CurrentGameState = newState;
    OnGameStateChanged?.Invoke(CurrentGameState);
  }
  public void SetPause(bool pause)
  {
    if (IsPause == pause) return;
    IsPause = pause;
    Time.timeScale = IsPause ? 0f : 1f;
    OnPauseChanged?.Invoke(IsPause);
  }
}