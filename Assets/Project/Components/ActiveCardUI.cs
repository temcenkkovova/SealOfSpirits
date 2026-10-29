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
      activeCard.OnActiveCard += ChangeActiveStatus;
    }
  }
  public void ChangeActiveStatus(bool status)
  {
    activeCardImage.gameObject.SetActive(status);
  }
  void OnDisable()
  {
    if (activeCard != null)
    {
      activeCard.OnActiveCard -= ChangeActiveStatus;
    }
  }
}