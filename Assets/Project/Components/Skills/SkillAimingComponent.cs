using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillAimingComponent : MonoBehaviour
{
  private float radius;
  public AimingCircle aimingCircle;

  private AimingCircle spawnedAimingCircle;
  public Vector2 MousePosition { get; private set; }
  public bool IsInAimingProcess { get; private set; }
  private PlayerInputController playerInputController;
  public Vector3 AimingCirclePosition { get; private set; }
  public event Action<Vector3> OnChoosePosition;

  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
  }
  void Start()
  {
    if (playerInputController)
      playerInputController.OnLeftButtonPressed += HandleFinishAiming;
  }
  void Update()
  {
    MousePosition = Mouse.current.position.ReadValue();
  }

  public void StartAiming(float radius)
  {
    StopAiming();
    IsInAimingProcess = true;
    this.radius = radius;
    Vector3 mouseDir = new Vector3();
    Ray ray = Camera.main.ScreenPointToRay(MousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    if (groundPlane.Raycast(ray, out float distance))
    {
      Vector3 mouseWorldPos = ray.GetPoint(distance);
      mouseDir = mouseWorldPos;
    }
    mouseDir.y = 0.1f;
    spawnedAimingCircle = Instantiate(aimingCircle, mouseDir, aimingCircle.transform.rotation);
    spawnedAimingCircle.transform.localScale = spawnedAimingCircle.transform.localScale * radius;
    spawnedAimingCircle.Init(this);
  }

  public void StopAiming()
  {
    IsInAimingProcess = false;
    if (spawnedAimingCircle)
    {
      Destroy(spawnedAimingCircle.gameObject);
    }
  }

  public void HandleFinishAiming()
  {
    if (!IsInAimingProcess) return;
    AimingCirclePosition = spawnedAimingCircle.transform.position;

    StopAiming();

    OnChoosePosition?.Invoke(AimingCirclePosition);
  }

  void OnDisable()
  {
    if (playerInputController)
      playerInputController.OnLeftButtonPressed -= HandleFinishAiming;
  }
}