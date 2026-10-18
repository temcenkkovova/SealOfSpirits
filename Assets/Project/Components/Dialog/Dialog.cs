using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{

  public TMP_Text titleField;
  public TMP_Text descriptionField;
  public Image image;

  public void Init(SkillItemUIConfig config)
  {
    titleField.text = config.title;
    descriptionField.text = config.description;
    image.sprite = config.image;

  }

  // void OnDestroy()
  // {
  //   OnDestroyDialog?.Invoke();
  // }
}