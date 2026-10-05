using System;
using UnityEngine;

public class ChaosComponent : MonoBehaviour
{
  public float CurrentChaos { get; private set; }
  public float requiredChaosToFail { get; private set; } = 100f;
  public event Action<float> OnCurrentChaosChanged;
  public event Action OnFailed;



  public void IncreaseChaos(float value)
  {
    CurrentChaos += value;
    OnCurrentChaosChanged?.Invoke(CurrentChaos);

    if (CurrentChaos >= requiredChaosToFail)
      OnFailed?.Invoke();


  }
  public void DecreaseChaos(float value)
  {
    CurrentChaos -= value;
    OnCurrentChaosChanged?.Invoke(CurrentChaos);
  }
}