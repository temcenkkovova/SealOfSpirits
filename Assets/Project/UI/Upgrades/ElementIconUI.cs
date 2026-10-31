using UnityEngine;
using UnityEngine.UI;

public class ElementIconUI : MonoBehaviour
{
  public Image imageField;

  public void Init(Sprite icon)
  {
    imageField.sprite = icon;
  }
}