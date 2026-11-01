using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public MenuActionController menuActionController;

  void Start()
  {
    if (menuActionController != null)
    {
      menuActionController.OnStartGamePressed += LoadGameplayScene;
      menuActionController.OnQuitPressed += ExitGame;
    }
  }
  public void LoadGameplayScene()
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

  public void ExitGame()
  {
    Application.Quit();
  }

  void OnDisable()
  {
    if (menuActionController != null)
    {
      menuActionController.OnStartGamePressed -= LoadGameplayScene;
      menuActionController.OnQuitPressed -= ExitGame;
    }
  }
}