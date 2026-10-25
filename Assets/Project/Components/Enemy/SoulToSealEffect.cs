using System.Collections.Generic;
using UnityEngine;

public class SoulToSealEffect : MonoBehaviour
{
  public Canvas canvas;
  public RectTransform canvasSealTr;
  public OrbEffect orbEffectPrefab;
  public Camera mainCamera;
  private float arrivalDepth = 2f;
  public ArriveOrbEffect arriveOrbEffect;
  private List<OrbEffect> createdOrbs = new List<OrbEffect>();
  void Awake()
  {
    if (mainCamera == null) mainCamera = Camera.main;
  }

  public void CreateOrbEffect(Vector3 createPosition)
  {
    OrbEffect orbEffect = Instantiate(orbEffectPrefab, createPosition, Quaternion.identity);
    createdOrbs.Add(orbEffect);
    orbEffect.Init(GetSealWorldPosition());
    orbEffect.OnArrived += HandleOrbArrived;
  }

  public Vector3 GetSealWorldPosition()
  {
    Canvas rootCanvas = canvas.rootCanvas;

    Camera uiCamera = rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera;

    Vector3 uiCenter = canvasSealTr.TransformPoint(canvasSealTr.rect.center);

    Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, uiCenter);

    return mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, arrivalDepth));


  }

  public void HandleOrbArrived(OrbEffect orb)
  {
    orb.OnArrived -= HandleOrbArrived;
    createdOrbs.Remove(orb);
    arriveOrbEffect.Play();
  }

  void OnDisable()
  {

  }
}