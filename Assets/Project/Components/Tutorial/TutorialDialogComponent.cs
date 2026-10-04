using UnityEngine;

public class TutorialDialogComponent : MonoBehaviour
{
  public Dialog dialogPrefab;
  public Transform dialogParent;
  private Dialog currentDialog;

  private string firstMessage = "Learn Meteor at the idol, then destroy the enemy before it reaches the seal!";
  private string secondMessage = "Learn Wave at the idol to push enemies back. Use it to keep them away from the seal!";
  private string thirdMessage = "Learn Stone at the idol to stun a group of enemies. Use it to buy time while Meteor recharges!";
  public TutorialComponent tutorialComponent;

  public void ShowDialog(int stageNumber)
  {
    string message = "";
    if (currentDialog != null) currentDialog = null;
    currentDialog = Instantiate(dialogPrefab, dialogParent);
    if (stageNumber == 0) message = firstMessage;
    if (stageNumber == 1) message = secondMessage;
    if (stageNumber == 2) message = thirdMessage;
    currentDialog.Init(message, 3f);
  }
  public void DestroyDialog()
  {
    Destroy(currentDialog.gameObject);
  }

}