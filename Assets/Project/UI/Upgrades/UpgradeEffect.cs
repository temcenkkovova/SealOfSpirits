using UnityEngine;

public class UpgradeEffect : MonoBehaviour
{


  public UpgradeElementIcon iconPrefab;
  public UpgradeManager upgradeManager;
  public Vector3 spawnVector = new Vector3(-5.12f, 7.13f, 4f);
  public RectTransform upgradesPanelRectTransform;
  public Canvas canvas;
  public Camera mainCamera;
  public float arrivalDepth = 2f;
  public ElementIconUI iconUIPrefab;
  private UpgradeElementIcon currentCreatedIcon;
  private UpgradeSkillUIConfig currentUpgradeUIConfig;

  void Start()
  {
    if (upgradeManager != null) upgradeManager.OnAddUpgrade += CreateElementIcon;
  }
  public void CreateElementIcon(UpgradeSkillUIConfig uiConfig)
  {
    currentCreatedIcon = Instantiate(iconPrefab, spawnVector, Quaternion.identity);
    currentCreatedIcon.Init(uiConfig.elementIcon, GetSealWorldPosition());
    currentCreatedIcon.OnArrived += CreateUIElementIcon;
    currentUpgradeUIConfig = uiConfig;
  }

  void OnDisable()
  {
    if (upgradeManager != null) upgradeManager.OnAddUpgrade -= CreateElementIcon;

    if (currentCreatedIcon != null) currentCreatedIcon.OnArrived -= CreateUIElementIcon;
  }
  public void CreateUIElementIcon()
  {
    ElementIconUI elementIcon = Instantiate(iconUIPrefab, upgradesPanelRectTransform);
    elementIcon.Init(currentUpgradeUIConfig.elementIcon);
  }
  public Vector3 GetSealWorldPosition()
  {
    Canvas rootCanvas = canvas.rootCanvas;

    Camera uiCamera = rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera;

    Vector3 uiCenter = upgradesPanelRectTransform.TransformPoint(upgradesPanelRectTransform.rect.center);

    Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, uiCenter);

    return mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, arrivalDepth));


  }
}