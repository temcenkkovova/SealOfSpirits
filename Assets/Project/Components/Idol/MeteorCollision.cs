using System;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
  private bool IsOnGround = false;
  public event Action OnGround;
  private float currentZ;
  private float currentX;
  private float duration;

  public void Init(float duration)
  {
    this.duration = duration;
  }
  void OnTriggerEnter(Collider other)
  {

    if (IsOnGround) return;
    if (other.gameObject.tag == "Ground")
    {
      IsOnGround = true;
      OnGround?.Invoke();

    }
  }

  void Start()
  {
    currentZ = transform.position.z;
    currentX = transform.position.x;
  }
  void Update()
  {
    if (IsOnGround) return;
    transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentX, 0f, currentZ), (duration - 2f) * Time.deltaTime);
  }
}