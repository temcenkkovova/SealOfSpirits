using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SealProgressUI : MonoBehaviour
{
  public TMP_Text currentProgressField;

  public SealComponent sealComponent;
  public Image fillImage;

  void Start()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged += ChangeProgress;
  }

  public void ChangeProgress(float amount)
  {
    float percentage = sealComponent.currentSealProgress / sealComponent.requiredSealProgress;
    currentProgressField.text = (percentage * 100).ToString() + " %";
    fillImage.fillAmount = percentage;
  }

  void OnDisable()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged -= ChangeProgress;
  }
}