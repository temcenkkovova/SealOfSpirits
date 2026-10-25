using System;
using TMPro;
using UnityEngine;

public class WaveSystemUI : MonoBehaviour
{
  public WaveTimer waveTimer;
  public GameObject timerPanel;
  public WaveSystem waveSystem;

  public TMP_Text textField;

  public TMP_Text requiredFaultsField;

  void Start()
  {
    if (waveTimer == null || GamePhaseController.Instance == null) return;


    waveTimer.OnTimerChanged += ShowTime;
    GamePhaseController.Instance.OnGamePhaseChanged += ShowTimerPanel;
    bool gamePhaseState = GamePhaseController.Instance.IsTutorialCompletedOnce;
    timerPanel.SetActive(gamePhaseState);

    if (waveSystem != null) waveSystem.OnRequiredFaultsChanged += ShowRequiredFaultsCount;
  }



  public void ShowRequiredFaultsCount(int required)
  {
    requiredFaultsField.text = required.ToString();
  }
  public void ShowTime(float time)
  {
    int number = (int)time;
    textField.text = FormatTime(number);
  }
  public string FormatTime(int value)
  {
    TimeSpan time = TimeSpan.FromSeconds(value);
    string formatted = time.ToString(@"mm\:ss");
    return formatted;
  }

  public void ShowTimerPanel(GamePhase gamePhase)
  {
    timerPanel.SetActive(gamePhase == GamePhase.Ritual);
  }

  void OnDisable()
  {

    if (waveTimer != null)
    {
      waveTimer.OnTimerChanged -= ShowTime;
    }


    if (GamePhaseController.Instance != null)
    {
      GamePhaseController.Instance.OnGamePhaseChanged -= ShowTimerPanel;
    }

    if (waveSystem != null) waveSystem.OnRequiredFaultsChanged -= ShowRequiredFaultsCount;

  }
}