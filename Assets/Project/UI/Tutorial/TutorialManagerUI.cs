using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManagerUI : MonoBehaviour
{
  public List<SkillItemUI> skillItemUIs = new List<SkillItemUI>();
  public TutorialDialogComponent dialogComponent;
  public TMP_Text numberStageField;
  void Start()
  {
    if (dialogComponent == null) return;
    dialogComponent.OnShowDialogSkill += ShowSkillsStatus;
  }

  public void ShowSkillsStatus(int number)
  {
    foreach (SkillItemUI item in skillItemUIs)
    {
      item.ChangeColor(number);
    }

    if (number == 0)
    {
      numberStageField.text = "Education - Step 1 from 3";
    }
    if (number == 1)
    {
      numberStageField.text = "Education - Step 2 from 3";
    }
    if (number == 2)
    {
      numberStageField.text = "Education - Step 3 from 3";
    }
  }

  void OnDisable()
  {
    if (dialogComponent == null) return;
    dialogComponent.OnShowDialogSkill -= ShowSkillsStatus;
  }
}