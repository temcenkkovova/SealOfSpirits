using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManagerUI : MonoBehaviour
{
  public List<SkillItemUI> skillItemUIs = new List<SkillItemUI>();
  public TutorialDialogComponent dialogComponent;
  public TMP_Text numberStageField;
  public GameObject tutorialPanel;
  void Start()
  {
    if (dialogComponent == null) return;
    dialogComponent.OnShowDialogSkill += ShowSkillsStatus;

    bool tutorialStatus = GamePhaseController.Instance.IsTutorialCompletedOnce;
    tutorialPanel.SetActive(!tutorialStatus);
  }

  public void ShowSkillsStatus(int number)
  {
    foreach (SkillItemUI item in skillItemUIs)
    {
      item.ChangeColor(number);
    }

    if (number == 0)
    {
      numberStageField.text = "Tutorial - Step 1 from 3";
    }
    if (number == 1)
    {
      numberStageField.text = "Tutorial - Step 2 from 3";
    }
    if (number == 2)
    {
      numberStageField.text = "Tutorial - Step 3 from 3";
    }
  }

  void OnDisable()
  {
    if (dialogComponent == null) return;
    dialogComponent.OnShowDialogSkill -= ShowSkillsStatus;
  }
}