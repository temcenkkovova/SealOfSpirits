using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
  public GameObject gameOverPanel;
  public TMP_Text textField;
  public CastSceneController castSceneController;
  void Start()
  {
    if (GamePhaseController.Instance == null || castSceneController == null) return;
    GamePhaseController.Instance.OnGamePhaseChanged += ShowGameOverPanel;
    castSceneController.OnShowDefeatedInfo += SetGameOverInfo;
  }

  void OnDisable()
  {
    if (GamePhaseController.Instance == null || castSceneController == null) return;
    GamePhaseController.Instance.OnGamePhaseChanged -= ShowGameOverPanel;
    castSceneController.OnShowDefeatedInfo -= SetGameOverInfo;
  }

  public void ShowGameOverPanel(GamePhase gamePhase)
  {
    bool isGameOver = gamePhase == GamePhase.Defeat;
    gameOverPanel.SetActive(isGameOver);

  }
  public void SetGameOverInfo(string info)
  {
    textField.text = info;
  }
}