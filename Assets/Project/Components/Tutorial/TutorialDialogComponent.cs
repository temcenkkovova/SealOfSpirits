
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogComponent : MonoBehaviour
{
  public Dialog dialogPrefab;
  public Transform dialogParent;
  private Dialog currentDialog;
  public GameObject dialogPanel;

  public List<SkillItemUIConfig> skillUIConfigs;


  public TutorialComponent tutorialComponent;

  public void ShowDialog(int stageNumber)
  {
    ChangeDialogPanelState(true);
    if (currentDialog != null)
    {
      Destroy(currentDialog.gameObject);
    }
    currentDialog = Instantiate(dialogPrefab, dialogParent);
    SkillItemUIConfig config = skillUIConfigs[stageNumber];
    currentDialog.Init(config);
  }
  public void DestroyDialog()
  {
    dialogParent.gameObject.SetActive(false);
    Destroy(currentDialog.gameObject);
  }


  public void ChangeDialogPanelState(bool state)
  {
    dialogPanel.gameObject.SetActive(state);
  }

}