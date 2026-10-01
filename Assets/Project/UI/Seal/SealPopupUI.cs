using System.Collections;
using TMPro;
using UnityEngine;

public class SealPopupUI : MonoBehaviour
{
  public PopupUI popup;
  public SealComponent sealComponent;
  private Vector3 lastPopupPos;



  void Start()
  {
    lastPopupPos = transform.position;
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged += ShowPopup;
  }
  public void ShowPopup(float amount)
  {
    Vector3 currentPopupPos = lastPopupPos + new Vector3(0, 1f, 0);
    PopupUI popupUI = Instantiate(popup, currentPopupPos, Quaternion.identity);
    popupUI.Init(amount);
    lastPopupPos = currentPopupPos;
  }

  void OnDisable()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged -= ShowPopup;
  }


}