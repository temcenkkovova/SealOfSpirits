using System;
using UnityEngine;

public class MenuActionController : MonoBehaviour
{
  public event Action OnSettings;
  public event Action OnQuitPressed;
  public event Action OnController;
  public event Action OnStartGamePressed;
  public event Action OnBackButtonPressed;

  public void HandleOpenSettings()
  {
    OnSettings?.Invoke();
  }
  public void HandleOpenController()
  {
    OnController?.Invoke();
  }
  public void HandleQuitGame()
  {
    OnQuitPressed?.Invoke();
  }
  public void HandleStartGame()
  {
    OnStartGamePressed?.Invoke();
  }
  public void HandleBackButton()
  {
    OnBackButtonPressed?.Invoke();
  }
}