using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChaosUI : MonoBehaviour
{
  public TMP_Text textField;
  public Image fillImage;
  public GameObject chaosPanel;

  public ChaosComponent chaosComponent;

  void Start()
  {
    chaosPanel.SetActive(false);
    if (chaosComponent == null || GamePhaseController.Instance == null) return;
    chaosComponent.OnCurrentChaosChanged += ShowChaos;
    GamePhaseController.Instance.OnGamePhaseChanged += ShowChaosPanel;
  }

  public void ShowChaos(float amount)
  {
    float percentage = amount / chaosComponent.requiredChaosToFail;
    textField.text = (percentage * 100).ToString() + " %";
    fillImage.fillAmount = percentage;
  }

  void OnDisable()
  {
    if (chaosComponent == null || GamePhaseController.Instance == null) return;
    chaosComponent.OnCurrentChaosChanged -= ShowChaos;
    GamePhaseController.Instance.OnGamePhaseChanged -= ShowChaosPanel;
  }
  public void ShowChaosPanel(GamePhase gamePhase)
  {
    if (gamePhase == GamePhase.Ritual)
    {
      chaosPanel.SetActive(true);
    }
  }
}