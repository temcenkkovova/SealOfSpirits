using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public PlayerInputController playerInputController;


  void Start()
  {
    if (playerInputController == null) return;
    playerInputController.OnPausePressed += HandleSetPause;
  }

  public void HandleSetPause(bool newStatus)
  {
    GameStateController.Instance.SetPause(newStatus);
  }

  void OnDisable()
  {
    if (playerInputController == null) return;
    playerInputController.OnPausePressed -= HandleSetPause;
  }
  public void RestartGame()
  {
    SceneManager.LoadScene("SampleScene");
  }
  public void HandleFinishGame()
  {
    GameStateController.Instance.ChangeGameState(GameState.Finished);
  }
}