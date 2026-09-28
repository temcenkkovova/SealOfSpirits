using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
  private PlayerInputs inputs;
  public Vector2 MoveInput { get; private set; }

  void Awake()
  {
    inputs = new PlayerInputs();
  }

  void Update()
  {
    MoveInput = inputs.Player.Move.ReadValue<Vector2>();
  }
  void OnEnable()
  {
    inputs.Enable();
  }
  void OnDisable()
  {
    inputs.Disable();
  }
}