using UnityEngine;
using UnityEngine.InputSystem;

public class AimingWave : MonoBehaviour
{
  public Vector2 MousePosition { get; private set; }
  public Vector3 waveTargetPosition;
  public SpriteRenderer visual;
  private float width = 3f;
  private SkillAimingComponent skillAimingComponent;
  public void Init(SkillAimingComponent skillAimingComponent)
  {
    this.skillAimingComponent = skillAimingComponent;
  }

  void Update()
  {
    MousePosition = Mouse.current.position.ReadValue();

    UpdateWaveAiming();
  }

  public void UpdateWaveAiming()
  {
    Camera mainCamera = Camera.main;

    Ray ray = mainCamera.ScreenPointToRay(MousePosition);

    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    if (!groundPlane.Raycast(ray, out float distance)) return;

    Vector3 start = skillAimingComponent.transform.position;
    start.y = 0.1f;
    waveTargetPosition = ray.GetPoint(distance);
    waveTargetPosition.y = 0.1f;

    Vector3 direction = waveTargetPosition - start;
    float length = direction.magnitude;
    if (length < 0.01f) return;

    Vector3 center = (start + waveTargetPosition) / 2f;
    center.y = 0.1f;

    transform.SetPositionAndRotation(
        center,
        Quaternion.LookRotation(direction, Vector3.up)
    );

    Vector3 spriteSize = visual.sprite.bounds.size;

    transform.localScale = new Vector3(
        width / spriteSize.x,
        1f,
        length / spriteSize.y
    );
  }
}