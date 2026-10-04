using UnityEngine;

public class TutorialDialogComponent : MonoBehaviour
{
  public Dialog dialogPrefab;
  public Transform dialogParent;
  private Dialog currentDialog;

  private string firstMessage = "Learn Meteor at the idol, then destroy the enemy before it reaches the seal!";
  private string secondMessage = "Learn Wave at the idol to push enemies back. Use it to keep them away from the seal!";
  private string thirdMessage = "Learn Stone at the idol to stun a group of enemies. Use it to buy time while Meteor recharges!";
  private float dialogNumber = 0;
  public TutorialComponent tutorialComponent;
  public PlayerSkills playerSkills;
  void Start()
  {
    ShowDialog();
    if (!tutorialComponent) return;
    tutorialComponent.OnEnemiesDead += ShowDialog;

    if (!playerSkills) return;
    playerSkills.OnSkillActivated += HandleFinishDialog;
  }
  public void ShowDialog()
  {
    if (dialogNumber >= 3) return;
    string message = "";
    if (currentDialog != null) currentDialog = null;
    currentDialog = Instantiate(dialogPrefab, dialogParent);
    if (dialogNumber == 0) message = firstMessage;
    if (dialogNumber == 1) message = secondMessage;
    if (dialogNumber == 2) message = thirdMessage;
    currentDialog.Init(message, 3f);

    dialogNumber++;
  }

  public void HandleFinishDialog(int numberSkill)
  {
    Destroy(currentDialog.gameObject);
    tutorialComponent.SpawnTutorialEnemy();
  }
  void OnDisable()
  {
    if (!tutorialComponent) return;
    tutorialComponent.OnEnemiesDead -= ShowDialog;
    if (!playerSkills) return;
    playerSkills.OnSkillActivated -= HandleFinishDialog;
  }
}