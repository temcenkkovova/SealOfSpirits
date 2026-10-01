using TMPro;
using UnityEngine;

public class SealProgressUI : MonoBehaviour
{
  public TMP_Text currentProgressField;

  public SealComponent sealComponent;

  void Start()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged += ChangeProgress;
  }

  public void ChangeProgress(float amount)
  {
    currentProgressField.text = sealComponent.currentSealProgress.ToString();
  }

  void OnDisable()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged -= ChangeProgress;
  }
}