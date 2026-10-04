
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
  public GameObject victoryPanel;

  void Start()
  {
    if (GamePhaseController.Instance == null) return;
    GamePhaseController.Instance.OnGamePhaseChanged += ShowVictoryPanel;
  }

  public void ShowVictoryPanel(GamePhase gamePhase)
  {
    bool isVictory = gamePhase == GamePhase.Victory;
    victoryPanel.SetActive(isVictory);
  }
  void OnDisable()
  {
    if (GamePhaseController.Instance == null) return;
    GamePhaseController.Instance.OnGamePhaseChanged -= ShowVictoryPanel;
  }
}