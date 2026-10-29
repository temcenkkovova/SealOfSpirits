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

  public void ShowUpgradesPanel()
  {

    foreach (GameObject go in gridParent)
    {
      Destroy(go);
    }
    cardsController.RemoveCardsList();
    upgradePanel.SetActive(true);
    List<UpgradeSkillConfig> randomConfigs = new List<UpgradeSkillConfig>();
    for (int i = 0; i <= 3; i++)
    {
      int randomNumber = Random.Range(0, upgradeController.upgradeSkillConfigs.Count);
      UpgradeSkillConfig config = upgradeController.upgradeSkillConfigs[randomNumber];
      if (!randomConfigs.Contains(config))
      {
        randomConfigs.Add(config);
      }
      else
      {
        config = upgradeController.upgradeSkillConfigs[Random.Range(0, upgradeController.upgradeSkillConfigs.Count)];
        if (!randomConfigs.Contains(config))
        {
          randomConfigs.Add(config);
        }
        else
        {
          config = upgradeController.upgradeSkillConfigs[Random.Range(0, upgradeController.upgradeSkillConfigs.Count)];
          if (!randomConfigs.Contains(config)) randomConfigs.Add(config);
        }

      }
    }


    foreach (UpgradeSkillConfig upgradeConfig in randomConfigs)
    {
      UpgradeSkillUIConfig upgradeConfigUI = upgradeConfig.upgradeSkillUIConfig;
      UpgradeCardUI cardUI = Instantiate(upgradeCardPrefab, gridParent);
      cardUI.Init(upgradeConfigUI, upgradeConfig);
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