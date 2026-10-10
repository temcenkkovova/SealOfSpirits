using UnityEngine;
using UnityEngine.InputSystem;

public class AimingCircle : MonoBehaviour
{
  private SkillAimingComponent skillAimingComponent;
  public Vector2 MousePosition { get; private set; }


  public void Init(SkillAimingComponent skillAimingComponent)
  {
    this.skillAimingComponent = skillAimingComponent;
  }




  void Update()
  {
    MousePosition = Mouse.current.position.ReadValue();

    Vector3 mouseDir = new Vector3();
    Ray ray = Camera.main.ScreenPointToRay(MousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    if (groundPlane.Raycast(ray, out float distance))
    {
      Vector3 mouseWorldPos = ray.GetPoint(distance);
      mouseDir = mouseWorldPos;
      mouseDir.y = 0.2f;
    }


    transform.position = mouseDir;

  }


}