using UnityEngine;
using UnityEngine.UI;

public class ActiveCardUI : MonoBehaviour
{
  public Image activeCardImage;

  public ActiveCard activeCard;

  void Start()
  {
    if (activeCard != null)
    {
      activeCard.OnClickedFirst += ChangeActiveCardImageStatus;
    }
  }
  public void ChangeActiveCardImageStatus()
  {
    activeCardImage.gameObject.SetActive(true);
  }
  void OnDisable()
  {
    if (activeCard != null)
    {
      activeCard.OnClickedFirst -= ChangeActiveCardImageStatus;
    }
  }
}