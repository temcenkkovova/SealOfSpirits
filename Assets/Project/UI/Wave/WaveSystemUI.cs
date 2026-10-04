using System;
using TMPro;
using UnityEngine;

public class WaveSystemUI : MonoBehaviour
{
  public WaveTimer waveTimer;


  public TMP_Text textField;

  void Start()
  {
    if (waveTimer == null) return;
    waveTimer.OnTimerChanged += ShowTime;
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
    if (waveTimer == null) return;
    waveTimer.OnTimerChanged -= ShowTime;
  }
}