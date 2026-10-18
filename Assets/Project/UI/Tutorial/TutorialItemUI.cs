
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialItemUI : MonoBehaviour
{
  public Image imageField;
  public TMP_Text titleField;
  public TMP_Text descriptionField;


  public void SetItem(SkillItemUIConfig educationItemConfig)
  {
    imageField.sprite = educationItemConfig.image;
    titleField.text = educationItemConfig.title;
    descriptionField.text = educationItemConfig.description;
  }
}