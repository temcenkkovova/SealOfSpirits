
using System;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
  private float waveDuration = 300;
  private float waveTimer;
  public event Action<float> OnTimerChanged;
  void Start()
  {

  }
  void Update()
  {
    waveTimer += Time.deltaTime;
    OnTimerChanged?.Invoke(waveTimer);

    if (waveTimer >= waveDuration)
    {
      Debug.Log("Finish game");
    }
  }

}