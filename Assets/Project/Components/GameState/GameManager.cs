using UnityEngine;

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
}