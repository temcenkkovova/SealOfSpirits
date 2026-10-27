using System.Collections.Generic;
using UnityEngine;

public class UpgradeControllerUI : MonoBehaviour
{
  public UpgradeCardUI upgradeCardPrefab;
  public UpgradeController upgradeController;
  public Transform gridParent;
  public GameObject upgradePanel;

  void Start()
  {
    if (upgradeController != null) upgradeController.OnShowUpgrade += ShowUpgradesPanel;
  }

  public void ShowUpgradesPanel()
  {
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
      cardUI.Init(upgradeConfigUI);
    }


  }

  public void CloseUpgradePanel()
  {
    upgradePanel.SetActive(false);
  }
  void OnDisable()
  {
    if (upgradeController != null) upgradeController.OnShowUpgrade -= ShowUpgradesPanel;
  }
}