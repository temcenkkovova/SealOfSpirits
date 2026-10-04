using System;
using UnityEngine;
public enum GameState { Gameplay, Pause, Dialogue }

public class GameStateController : MonoBehaviour
{
  public GameState CurrentGameState { get; private set; }

  public event Action<GameState> OnGameStateChanged;
  public static GameStateController Instance;

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

}