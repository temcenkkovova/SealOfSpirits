using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
  public LayerMask layerMask;

  void OnTriggerEnter(Collider other)
  {
    if (!IsInteractMask(other.gameObject)) return;

    IInteractable interactable = other.GetComponent<IInteractable>();
    interactable.Interact();
  }

  public bool IsInteractMask(GameObject target)
  {
    return (layerMask.value & (1 << target.layer)) != 0;
  }
}