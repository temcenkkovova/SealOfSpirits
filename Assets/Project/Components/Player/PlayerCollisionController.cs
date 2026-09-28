using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
  public LayerMask layerMask;
  private IInteractable currentInteractable;
  private PlayerInputController playerInputController;

  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
  }

  void Start()
  {
    if (playerInputController == null) return;
    playerInputController.OnPressInteractButton += HandlePressInteractButton;
  }
  void OnTriggerEnter(Collider other)
  {
    if (!IsInteractMask(other.gameObject)) return;

    IInteractable interactable = other.GetComponent<IInteractable>();
    currentInteractable = interactable;
  }


  void OnTriggerExit(Collider other)
  {
    if (!IsInteractMask(other.gameObject)) return;
    currentInteractable = null;
  }
  public bool IsInteractMask(GameObject target)
  {
    return (layerMask.value & (1 << target.layer)) != 0;
  }

  public void HandlePressInteractButton()
  {
    if (currentInteractable == null) return;
    currentInteractable.Interact();
  }

  void OnDisable()
  {
    if (playerInputController == null) return;
    playerInputController.OnPressInteractButton -= HandlePressInteractButton;
  }
}