using System;
using UnityEngine;
public enum GamePhase
{
  Tutorial, Ritual, Victory, Defeat
}
public class GamePhaseController : MonoBehaviour
{
  public GamePhase CurrentGamePhase { get; private set; }
  public event Action<GamePhase> OnGamePhaseChanged;
  public static GamePhaseController Instance;
  public bool IsTutorialCompletedOnce = false;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }


  void Start()
  {
    if (IsTutorialCompletedOnce)
    {
      ChangeGamePhase(GamePhase.Ritual);
    }
    else
    {
      ChangeGamePhase(GamePhase.Tutorial);
    }


  }
  public void InitPhase()
  {
    if (IsTutorialCompletedOnce)
    {
      ChangeGamePhase(GamePhase.Ritual);
      Debug.Log("Here");
    }
    else
    {
      ChangeGamePhase(GamePhase.Tutorial);
    }
  }
  public void ChangeGamePhase(GamePhase newPhase)
  {
    if (newPhase == CurrentGamePhase) return;
    CurrentGamePhase = newPhase;
    OnGamePhaseChanged?.Invoke(CurrentGamePhase);
  }

  public void HandleFinishTutorial()
  {
    ChangeGamePhase(GamePhase.Ritual);
    IsTutorialCompletedOnce = true;

  }
}