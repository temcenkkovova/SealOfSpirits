using System;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
  public bool IsCollisionWithWall { get; private set; } = false;

  public LayerMask wallLayer;

  void OnTriggerEnter(Collider other)
  {
    if (IsInteractMask(other.gameObject))
    {

      IsCollisionWithWall = true;

    }
  }

  void OnTriggerExit(Collider other)
  {
    if (IsInteractMask(other.gameObject))
    {
      IsCollisionWithWall = false;

    }
  }
  public bool IsInteractMask(GameObject target)
  {
    return (wallLayer.value & (1 << target.layer)) != 0;
  }
}