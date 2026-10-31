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