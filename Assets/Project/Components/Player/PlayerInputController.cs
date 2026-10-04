using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
  private PlayerInputs inputs;
  public Vector2 MoveInput { get; private set; }
  public event Action OnPressInteractButton;
  public event Action<int> OnSkillPressed;
  public event Action OnLeftButtonPressed;
  public event Action<bool> OnPausePressed;

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
    inputs.Player.FirstSkill.performed += PressFirstSkillButton;
    inputs.Player.SecondSkill.performed += PressSecondSkillButton;
    inputs.Player.ThirdSkill.performed += PressThirdSkillButton;
    inputs.Player.ConfirmSkill.performed += PressLeftButton;
    inputs.Player.Pause.performed += PressPauseButton;
  }
  void OnDisable()
  {

    inputs.Player.Interact.performed -= PressInteractButton;
    inputs.Player.FirstSkill.performed -= PressFirstSkillButton;
    inputs.Player.SecondSkill.performed -= PressSecondSkillButton;
    inputs.Player.ThirdSkill.performed -= PressThirdSkillButton;
    inputs.Player.ConfirmSkill.performed -= PressLeftButton;
    inputs.Player.Pause.performed -= PressPauseButton;
    inputs.Disable();
  }
  public void PressFirstSkillButton(InputAction.CallbackContext context)
  {
    OnSkillPressed?.Invoke(0);
  }
  public void PressSecondSkillButton(InputAction.CallbackContext context)
  {
    OnSkillPressed?.Invoke(1);
  }
  public void PressThirdSkillButton(InputAction.CallbackContext context)
  {
    OnSkillPressed?.Invoke(2);
  }
  public void PressInteractButton(InputAction.CallbackContext context)
  {
    OnPressInteractButton?.Invoke();
  }
  public void PressLeftButton(InputAction.CallbackContext context)
  {
    OnLeftButtonPressed?.Invoke();
  }
  public void PressPauseButton(InputAction.CallbackContext context)
  {
    OnPausePressed?.Invoke(!GameStateController.Instance.IsPause);
  }
}