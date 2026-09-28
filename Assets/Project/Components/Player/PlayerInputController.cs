using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
  private PlayerInputs inputs;
  public Vector2 MoveInput { get; private set; }
  public event Action OnPressInteractButton;

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
    inputs.Player.Interact.performed += PressInteractButton;
  }
  void OnDisable()
  {
    inputs.Disable();
    inputs.Player.Interact.performed -= PressInteractButton;
  }

  public void PressInteractButton(InputAction.CallbackContext context)
  {
    OnPressInteractButton?.Invoke();
  }
}