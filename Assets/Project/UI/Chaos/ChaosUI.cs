using TMPro;
using UnityEngine;

public class ChaosUI : MonoBehaviour
{
  public TMP_Text textField;


  public ChaosComponent chaosComponent;

  void Start()
  {
    if (chaosComponent == null) return;
    chaosComponent.OnCurrentChaosChanged += ShowChaos;
  }

  public void ShowChaos(float amount)
  {
    textField.text = amount.ToString();
  }

  void OnDisable()
  {
    if (chaosComponent == null) return;
    chaosComponent.OnCurrentChaosChanged -= ShowChaos;
  }
}