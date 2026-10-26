using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public void LoadMainScene()
  {
    SceneManager.LoadScene("Main");
    // GamePhaseController.Instance.ChangeGamePhase(GamePhase.Ritual);
    // GameStateController.Instance.ChangeGameState(GameState.Gameplay);
  }

  public void LoadMenuScene()
  {
    SceneManager.LoadScene("Menu");
    GameStateController.Instance.ChangeGameState(GameState.Finished);

  }
}