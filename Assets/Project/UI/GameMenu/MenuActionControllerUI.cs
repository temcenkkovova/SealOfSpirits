using UnityEngine;

public class MenuActionControllerUI : MonoBehaviour
{
  public Transform settingsPanel;
  public Transform authorsPanel;
  public Transform backButton;
  public MenuActionController menuController;
  public Transform menuButtonsPanel;

  void Start()
  {
    if (menuController != null)
    {
      menuController.OnController += OpenAuthorsPanel;
      menuController.OnSettings += OpenSettingsPanel;
      menuController.OnBackButtonPressed += MoveBackToMenu;
    }
  }

  void OnEnable()
  {
    if (menuController == null) return;
    menuController.OnController += OpenAuthorsPanel;
    menuController.OnSettings += OpenSettingsPanel;
    menuController.OnBackButtonPressed += MoveBackToMenu;
  }

  public void MoveBackToMenu()
  {
    backButton.gameObject.SetActive(false);
    authorsPanel.gameObject.SetActive(false);
    settingsPanel.gameObject.SetActive(false);
    menuButtonsPanel.gameObject.SetActive(true);
  }
  public void OpenAuthorsPanel()
  {
    menuButtonsPanel.gameObject.SetActive(false);
    backButton.gameObject.SetActive(true);
    authorsPanel.gameObject.SetActive(true);
    settingsPanel.gameObject.SetActive(false);
  }
  public void OpenSettingsPanel()
  {
    menuButtonsPanel.gameObject.SetActive(false);
    backButton.gameObject.SetActive(true);
    settingsPanel.gameObject.SetActive(true);

  }

  void OnDisable()
  {
    if (menuController != null)
    {
      menuController.OnController -= OpenAuthorsPanel;
      menuController.OnSettings -= OpenSettingsPanel;
      menuController.OnBackButtonPressed -= MoveBackToMenu;
    }
  }
}