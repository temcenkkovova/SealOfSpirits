using System;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
  private float waveDuration = 300;
  private float waveTimer;
  public event Action<float> OnTimerChanged;
  public event Action OnTimerFinished;

  void Update()
  {
    if (!IsPlayingGame()) return;
    waveTimer += Time.deltaTime;
    OnTimerChanged?.Invoke(waveTimer);

    if (waveTimer >= waveDuration)
    {
      OnTimerFinished?.Invoke();

    }
  }
  public bool IsPlayingGame()
  {

    return GamePhaseController.Instance.CurrentGamePhase == GamePhase.Ritual && GameStateController.Instance.CurrentGameState == GameState.Gameplay;

  }
}