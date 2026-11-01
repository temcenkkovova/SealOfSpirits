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
    GamePhaseController.Instance.InitPhase();
    GameStateController.Instance.ChangeGameState(GameState.Gameplay);
  }

  public void LoadMenuScene()
  {
    GameStateController.Instance.SetPause(false);
    GameStateController.Instance.ChangeGameState(GameState.Finished);
    SceneManager.LoadScene("Menu");


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