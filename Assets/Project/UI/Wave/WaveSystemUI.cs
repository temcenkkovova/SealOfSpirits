using System;
using TMPro;
using UnityEngine;

public class WaveSystemUI : MonoBehaviour
{
  public WaveSystem waveSystem;

  public TMP_Text textField;

  void Start()
  {
    if (waveSystem == null) return;
    waveSystem.OnTimerChanged += ShowTime;
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

  void OnDisable()
  {
    if (waveSystem == null) return;
    waveSystem.OnTimerChanged -= ShowTime;
  }
}