using System.Collections;
using TMPro;
using UnityEngine;

public class SealPopupUI : MonoBehaviour
{
  public TMP_Text textField;
  public SealComponent sealComponent;
  private float currentTotalAmount;


  void Start()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged += ShowAmount;
  }
  public void ShowAmount(float amount)
  {
    if (currentTotalAmount > 0)
    {
      currentTotalAmount += amount;
      textField.text = currentTotalAmount.ToString();
    }
    else
    {
      currentTotalAmount = amount;
      textField.text = currentTotalAmount.ToString();
    }


    StartCoroutine(hideShowedText());
  }

  void OnDisable()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged -= ShowAmount;
  }

  IEnumerator hideShowedText()
  {
    yield return new WaitForSeconds(2f);
    textField.text = "";
    currentTotalAmount = 0;
  }
}