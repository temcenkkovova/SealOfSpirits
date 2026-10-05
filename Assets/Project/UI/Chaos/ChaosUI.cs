using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChaosUI : MonoBehaviour
{
  public TMP_Text textField;
  public Image fillImage;


  public ChaosComponent chaosComponent;

  void Start()
  {
    if (chaosComponent == null) return;
    chaosComponent.OnCurrentChaosChanged += ShowChaos;
  }

  public void ShowChaos(float amount)
  {
    float percentage = amount / chaosComponent.requiredChaosToFail;
    textField.text = (percentage * 100).ToString() + " %";
    fillImage.fillAmount = percentage;
  }

  void OnDisable()
  {
    if (chaosComponent == null) return;
    chaosComponent.OnCurrentChaosChanged -= ShowChaos;
  }
}