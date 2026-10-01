using System;
using UnityEngine;

public class SealComponent : MonoBehaviour
{
  public float currentSealProgress { get; private set; } = 0;
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

    OnCurrentSealProgressChanged?.Invoke(amount);
  }
}