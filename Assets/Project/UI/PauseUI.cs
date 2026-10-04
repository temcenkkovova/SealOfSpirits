using UnityEngine;

public class PauseUI : MonoBehaviour
{
  public GameObject pausePanel;


  void Start()
  {
    if (GameStateController.Instance == null) return;
    GameStateController.Instance.OnPauseChanged += ShowPausePanel;
  }

  public void ShowPausePanel(bool pauseStatus)
  {
    pausePanel.SetActive(pauseStatus);
  }

  void OnDisable()
  {
    if (GameStateController.Instance == null) return;
    GameStateController.Instance.OnPauseChanged -= ShowPausePanel;
  }
}