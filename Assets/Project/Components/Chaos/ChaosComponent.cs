using System;
using UnityEngine;

public class ChaosComponent : MonoBehaviour
{
  public float CurrentChaos { get; private set; }
  public float requiredChaosToFail { get; private set; } = 100f; // 100f
  public event Action<float> OnCurrentChaosChanged;
  public event Action OnFailed;
  public float currentMultiplyChaos { get; private set; } = 0f;
  private float maxMultiplyChaos = 6; // 6 sec
  private float totalMultiplyChaos;

  void Update()
  {
    IncreaseChaosValue(currentMultiplyChaos * Time.deltaTime); // Check it
  }
  public void IncreaseChaosMultiply(float multiply)
  {
    if (IsTutorialStage()) return;
    totalMultiplyChaos += multiply;

    currentMultiplyChaos = totalMultiplyChaos <= maxMultiplyChaos ? totalMultiplyChaos : maxMultiplyChaos;

  }
  public void DecreaseChaosMultiply(float multiply)
  {
    if (IsTutorialStage()) return;
    totalMultiplyChaos -= multiply;
    if (totalMultiplyChaos < 0) totalMultiplyChaos = 0;
    currentMultiplyChaos = totalMultiplyChaos <= maxMultiplyChaos ? totalMultiplyChaos : maxMultiplyChaos;
  }

  public void ResetChaosMultiply()
  {
    currentMultiplyChaos = 0;
    totalMultiplyChaos = 0;
  }


  public void IncreaseChaosValue(float value)
  {
    if (IsTutorialStage()) return;
    CurrentChaos += value;
    OnCurrentChaosChanged?.Invoke(CurrentChaos);

    if (CurrentChaos >= requiredChaosToFail)
      OnFailed?.Invoke();
  }
  public void DecreaseChaosValue(float value)
  {
    if (IsTutorialStage()) return;
    if (CurrentChaos <= 0) return;
    CurrentChaos -= value;
    OnCurrentChaosChanged?.Invoke(CurrentChaos);
  }
  public bool IsTutorialStage()
  {

    return GamePhaseController.Instance.CurrentGamePhase == GamePhase.Tutorial;
  }
}