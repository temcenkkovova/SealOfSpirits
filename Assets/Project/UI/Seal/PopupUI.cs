using System;
using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
  public TMP_Text textField;
  public event Action OnDestroyPopup;

  public void Init(float amount)
  {
    textField.text = "+ " + amount.ToString();
  }

  void Start()
  {
    Destroy(gameObject, 1.5f);
  }

  void OnDestroy()
  {
    OnDestroyPopup?.Invoke();
  }
}