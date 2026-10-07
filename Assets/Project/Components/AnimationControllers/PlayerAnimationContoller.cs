using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
  private PlayerMovement playerMovement;
  public Animator playerAnimator;

  void Awake()
  {
    playerMovement = GetComponent<PlayerMovement>();
  }
  void Update()
  {
    if (playerAnimator == null || playerMovement == null) return;

    playerAnimator.SetFloat("MoveSpeed", playerMovement.MovePercentage);
  }


}