using TMPro;
using UnityEngine;

public class IdolInteractVisualUI : MonoBehaviour
{
  public IdolSkillController idolSkillController;
  public TMP_Text titleField;

  void Awake()
  {
    if (idolSkillController == null) return;
    idolSkillController.OnSetNewInteractVisual += ShowNewIdolTitle;
  }

  public void ShowNewIdolTitle(string newTitle)
  {
    titleField.text = newTitle;
  }

  void OnDisable()
  {
    if (idolSkillController == null) return;
    idolSkillController.OnSetNewInteractVisual -= ShowNewIdolTitle;
  }
}