using System;
using UnityEngine;

public class SealComponent : MonoBehaviour
{
  private float currentSealProgress = 0;
  private float requiredSealProgress = 100f;
  public event Action<float> OnCurrentSealProgressChanged;
  public event Action OnSealProgressCompleted;



  public void AddProgressAmount(float amount)
  {
    currentSealProgress += amount;
    if (currentSealProgress >= requiredSealProgress)
    {
      OnSealProgressCompleted?.Invoke();
    }

    OnCurrentSealProgressChanged?.Invoke(currentSealProgress);
  }
}