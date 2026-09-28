using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public float MoveSpeed { get; private set; }
  private PlayerInputController playerInputController;
  private CharacterController characterController;

  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    characterController = GetComponent<CharacterController>();
  }

  public void Init(float speed)
  {
    MoveSpeed = speed;
  }

  void Update()
  {
    Vector3 movePos = new Vector3(playerInputController.MoveInput.x, 0f, playerInputController.MoveInput.y);
    if (movePos.sqrMagnitude < 0.01f) return;

    characterController.Move(movePos * MoveSpeed * Time.deltaTime);
  }
}