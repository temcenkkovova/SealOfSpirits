using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillAimingComponent : MonoBehaviour
{
  private float radius;
  public AimingCircle aimingCircle;
  public AimingWave aimingWave;
  private AimingCircle spawnedAimingCircle;
  private AimingWave spawnedAimingWave;
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

  public void StartAimingMeteor(float radius)
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

  public void StartAimingWave(float radius)
  {
    StopAiming();
    IsInAimingProcess = true;
    this.radius = radius;

    spawnedAimingWave = Instantiate(aimingWave, transform.position, aimingWave.transform.rotation);
    spawnedAimingWave.transform.localScale = spawnedAimingWave.transform.localScale * radius;
    spawnedAimingWave.Init(this);
  }

  public void StopAiming()
  {
    IsInAimingProcess = false;

    if (spawnedAimingCircle != null)
    {
      Destroy(spawnedAimingCircle.gameObject);
      spawnedAimingCircle = null;
    }

    if (spawnedAimingWave != null)
    {
      Destroy(spawnedAimingWave.gameObject);
      spawnedAimingWave = null;
    }
  }

  public void HandleFinishAiming()
  {
    if (!IsInAimingProcess) return;

    if (spawnedAimingWave != null)
    {
      AimingCirclePosition = spawnedAimingWave.waveTargetPosition;
    }
    else if (spawnedAimingCircle != null)
    {
      AimingCirclePosition = spawnedAimingCircle.transform.position;
    }
    else
    {
      return;
    }

    StopAiming();
    OnChoosePosition?.Invoke(AimingCirclePosition);
  }

  void OnDisable()
  {
    if (playerInputController)
      playerInputController.OnLeftButtonPressed -= HandleFinishAiming;
  }
}