using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
  public bool isTutorialFinished { get; private set; } = false;
  public event Action OnFinishTutorial;
  private int tutorialStage = 0;
  private int maxCountStage = 3;
  public PlayerSkills playerSkills;

  public TutorialDialogComponent tutorialDialogComponent;
  public TutorialComponent tutorialComponent;


  void Start()
  {
    if (GamePhaseController.Instance.IsTutorialCompletedOnce)
    {
      playerSkills.ActivateAllSkills();
      return;
    }
    PlayDialog();

    if (playerSkills == null) return;
    playerSkills.OnSkillActivated += HandleHideDialog;
    if (tutorialComponent == null) return;
    tutorialComponent.OnEnemiesDead += HandleCompletedStage;
  }
  void PlayDialog()
  {
    if (tutorialStage >= maxCountStage) return;
    tutorialDialogComponent.ShowDialog(tutorialStage);
  }

  public void HandleHideDialog(int numberSkill)
  {

    if (numberSkill != tutorialStage) return;
    if (tutorialStage >= maxCountStage) return;
    tutorialDialogComponent.DestroyDialog();
    tutorialComponent.SpawnTutorialEnemy(tutorialStage);
  }
  public void HandleCompletedStage()
  {
    tutorialStage++;
    playerSkills.ChangeNumberLearned();
    if (tutorialStage >= maxCountStage)
    {
      isTutorialFinished = true;
      tutorialComponent.HandleDestroyTutorialFaults();
      GamePhaseController.Instance.HandleFinishTutorial();

    }
    else
    {
      PlayDialog();
    }

  }

  void OnDisable()
  {
    if (playerSkills == null) return;
    playerSkills.OnSkillActivated -= HandleHideDialog;
    if (tutorialComponent == null) return;
    tutorialComponent.OnEnemiesDead -= HandleCompletedStage;
  }
}