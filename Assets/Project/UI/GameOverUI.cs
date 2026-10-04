using UnityEngine;

public class GameOverUI : MonoBehaviour
{
  public GameObject gameOverPanel;
  void Start()
  {
    if (GameStateController.Instance == null) return;
    GameStateController.Instance.OnGameStateChanged += ShowGameOverPanel;
  }

  void OnDisable()
  {
    if (GameStateController.Instance == null) return;
    GameStateController.Instance.OnGameStateChanged -= ShowGameOverPanel;
  }

  public void ShowGameOverPanel(GameState gameState)
  {
    bool isGameOver = gameState == GameState.GameOver;
    gameOverPanel.SetActive(isGameOver);

  }
}