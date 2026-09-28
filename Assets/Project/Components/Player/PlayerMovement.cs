using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public float MoveSpeed { get; private set; }
  public void Init(float speed)
  {
    MoveSpeed = speed;
  }
}