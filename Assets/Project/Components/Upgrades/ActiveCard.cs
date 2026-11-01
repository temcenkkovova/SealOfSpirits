using System;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCard : MonoBehaviour
{
  public event Action<ActiveCard> OnClickCard;
  public event Action<bool> OnActiveCard;
  private Button button;
  private bool firstClick;


  void Awake()
  {
    button = GetComponent<Button>();
  }
  public void SetActiveStatus(bool newStatus)
  {
    OnActiveCard?.Invoke(newStatus);
  }

  void Start()
  {
    button.onClick.AddListener(HandleClick);
  }

  public void HandleClick()
  {
    if (!firstClick)
      OnClickCard?.Invoke(this);
  }

  void OnDisable()
  {
    button.onClick.RemoveListener(HandleClick);
  }
}