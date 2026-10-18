using System.Collections;
using TMPro;
using UnityEngine;

public class SealPopupUI : MonoBehaviour
{
  public PopupUI popup;
  public SealComponent sealComponent;
  private Vector3 lastPopupPos;
  private PopupUI currentPopup;


  void Start()
  {
    lastPopupPos = transform.position;
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged += ShowPopup;
  }
  public void ShowPopup(float amount)
  {
    Vector3 currentPopupPos = lastPopupPos + new Vector3(0, 1f, 0);
    currentPopup = Instantiate(popup, currentPopupPos, Quaternion.identity);
    currentPopup.Init(amount);
    currentPopup.OnDestroyPopup += ClearCurrentPopup;
    lastPopupPos = currentPopupPos;
  }

  public void ClearCurrentPopup()
  {
    if (currentPopup == null) return;
    currentPopup.OnDestroyPopup -= ClearCurrentPopup;
    currentPopup = null;
    lastPopupPos = transform.position;
  }
  void OnDisable()
  {
    if (sealComponent == null) return;
    sealComponent.OnCurrentSealProgressChanged -= ShowPopup;
  }


}