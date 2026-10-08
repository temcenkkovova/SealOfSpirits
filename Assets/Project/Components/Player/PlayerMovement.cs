using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public float MoveSpeed { get; private set; }
  public float SprintSpeed { get; private set; } = 7f;
  private PlayerInputController playerInputController;
  private float speedRotation = 10f;
  private CharacterController characterController;
  private PlayerStatus playerStatus;
  private bool isSprinting = false;

  public float MovePercentage { get; private set; }
  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    characterController = GetComponent<CharacterController>();
    playerStatus = GetComponent<PlayerStatus>();

  }
  void Start()
  {
    if (playerInputController == null) return;
    playerInputController.OnSprintChanged += ChangeSprintStatus;
  }

  public void Init(float speed)
  {
    MoveSpeed = speed;
  }

  void Update()
  {
    Vector3 movePos = new Vector3(playerInputController.MoveInput.x, 0f, playerInputController.MoveInput.y);
    if (movePos.sqrMagnitude < 0.01f || playerStatus.IsStunned)
    {
      MovePercentage = 0f;
      return;
    }
    float speed = isSprinting ? SprintSpeed : MoveSpeed;
    MovePercentage = movePos.sqrMagnitude > 0.01f ? (isSprinting ? 1f : 0.5f) : 0;
    movePos.y = 0f;
    Quaternion targetRotation = Quaternion.LookRotation(movePos);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    characterController.Move(movePos * speed * Time.deltaTime);
  }

  public void ChangeSprintStatus(bool newStatus)
  {
    isSprinting = newStatus;
  }

  void OnDisable()
  {
    if (playerInputController == null) return;
    playerInputController.OnSprintChanged -= ChangeSprintStatus;
  }
}