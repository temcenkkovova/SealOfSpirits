using System;
using System.Collections.Generic;
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
  public List<IdolBootstrap> idolBootstraps = new List<IdolBootstrap>();


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
    tutorialDialogComponent.ChangeDialogPanelState(false);
    tutorialComponent.SpawnTutorialEnemy(tutorialStage);
  }
  public void HandleCompletedStage()
  {
    tutorialStage++;
    playerSkills.ChangeNumberLearned();
    if (tutorialStage >= maxCountStage)
    {
      isTutorialFinished = true;
      tutorialDialogComponent.ChangeDialogPanelState(false);
      tutorialDialogComponent.DestroyDialog();
      foreach (IdolBootstrap idolBootstrap in idolBootstraps)
      {
        Destroy(idolBootstrap.gameObject);
      }

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