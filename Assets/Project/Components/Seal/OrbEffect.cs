using System;
using UnityEngine;

public class OrbEffect : MonoBehaviour
{

  private Vector3 direction;
  public event Action<OrbEffect> OnArrived;
  public void Init(Vector3 direction)
  {
    this.direction = direction;
  }

  void Update()
  {
    if (direction == null) return;
    transform.position = Vector3.MoveTowards(
     transform.position,
      direction,
      40f * Time.deltaTime);

    Vector3 distance = transform.position - direction;
    distance.y = 0f;
    if (distance.sqrMagnitude <= 0.010)
    {
      Debug.Log("Play Shine Animation");
      OnArrived?.Invoke(this);
      Destroy(gameObject, 0.5f);
    }
  }


}