using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
  public TMP_Text title;
  public Image image;
  public TMP_Text numberField;
  public int ownNumber;
  public void ChangeColor(int number)
  {
    if (number == ownNumber)
    {
      title.color = Color.yellow;
      image.color = Color.yellow;
      numberField.color = Color.yellow;
      return;
    }

    if (number > ownNumber)
    {
      title.color = Color.green;
      image.color = Color.green;
      numberField.color = Color.green;
      return;
    }
    if (number < ownNumber)
    {
      title.color = Color.gray;
      image.color = Color.gray;
      numberField.color = Color.gray;
      return;
    }

  }
}