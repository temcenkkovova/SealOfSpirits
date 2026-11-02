using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControllerUI : MonoBehaviour
{
  public UpgradeCardUI upgradeCardPrefab;
  public UpgradeController upgradeController;
  public Transform gridParent;
  public GameObject upgradePanel;
  public CardsController cardsController;

  public Button confirmActiveCardButton;


  public List<GameObject> upgradesRombs = new List<GameObject>();
  public Image fillImage;

  void Start()
  {
    if (upgradeController != null) upgradeController.OnShowUpgrade += ShowUpgradesPanel;

    if (cardsController != null)
    {
      cardsController.OnCurrentActiveCard += ChangeStatusConfirmButton;
      cardsController.OnConfirmedUpgrade += CloseUpgradePanel;
    }
  }

  public void ShowUpgradesPanel(List<UpgradeSkillConfig> upgradesList)
  {
    if (upgradesList == null) return;
    int upgradeIndex = 0;

    foreach (GameObject romb in upgradesRombs)
    {
      romb.SetActive(false);
    }
    foreach (Transform childTr in gridParent.GetComponentInChildren<Transform>())
    {
      Destroy(childTr.gameObject);
    }
    cardsController.RemoveCardsList();
    upgradePanel.SetActive(true);

    foreach (UpgradeSkillConfig upgradeConfig in upgradesList)
    {
      upgradeIndex++;
      UpgradeSkillUIConfig upgradeConfigUI = upgradeConfig.upgradeSkillUIConfig;
      UpgradeCardUI cardUI = Instantiate(upgradeCardPrefab, gridParent);
      cardUI.Init(upgradeConfigUI, upgradeConfig, upgradeIndex);

      if (cardsController != null)
        cardsController.AddNewCards(cardUI);
    }

    for (int i = 0; i < upgradeController.nextUpgradeTime; i++)
    {
      upgradesRombs[i].SetActive(true);
    }
    fillImage.fillAmount = (float)upgradeController.nextUpgradeTime / upgradeController.upgradesTime.Length;
    Debug.Log(upgradeController.upgradesTime.Length);

  }

  public void CloseUpgradePanel()
  {
    upgradePanel.SetActive(false);
  }
  void OnDisable()
  {
    if (upgradeController != null) upgradeController.OnShowUpgrade -= ShowUpgradesPanel;

    if (cardsController != null)
    {
      cardsController.OnCurrentActiveCard -= ChangeStatusConfirmButton;
      cardsController.OnConfirmedUpgrade -= CloseUpgradePanel;
    }
  }

  public void ChangeStatusConfirmButton(bool status)
  {
    confirmActiveCardButton.interactable = status;
  }
}