using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerUI : MonoBehaviour
{
  public List<SkillItemUI> skillItemUIs = new List<SkillItemUI>();
  public TutorialDialogComponent dialogComponent;
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
  }

  void OnDisable()
  {
    if (dialogComponent == null) return;
    dialogComponent.OnShowDialogSkill -= ShowSkillsStatus;
  }
}