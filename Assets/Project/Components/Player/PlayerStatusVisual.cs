using UnityEngine;

public class PlayerStatusVisual : MonoBehaviour
{


  private PlayerStatus playerStatus;
  public GameObject visualStatus;

  void Awake()
  {
    playerStatus = GetComponent<PlayerStatus>();
  }

  void Start()
  {
    if (playerStatus == null) return;
    playerStatus.OnStunStatusChanged += ShowStatusVisual;
  }

  public void ShowStatusVisual(bool status)
  {
    if (status)
    {
      visualStatus.SetActive(true);
    }
    else
    {
      visualStatus.SetActive(false);
    }
  }

  void OnDisable()
  {
    if (playerStatus == null) return;
    playerStatus.OnStunStatusChanged -= ShowStatusVisual;
  }
}