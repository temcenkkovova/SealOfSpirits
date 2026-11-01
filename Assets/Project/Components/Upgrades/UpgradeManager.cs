using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
  public List<UpgradeSkillConfig> currentUpgrades = new List<UpgradeSkillConfig>();
  public UpgradeController upgradeController;
  public PlayerSkills playerSkills;
  public event Action<UpgradeSkillUIConfig> OnAddUpgrade;

  public void AddSkillUpgrade(UpgradeSkillConfig upgradeSkillConfig)
  {
    if (!currentUpgrades.Contains(upgradeSkillConfig))
    {
      currentUpgrades.Add(upgradeSkillConfig);
      switch (upgradeSkillConfig.upgradeStat)
      {
        case UpgradeStats.Radius:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == UpgradeElements.Meteor)
            {
              UpgradeMeteor upgradeMeteor = upgradeSkillConfig as UpgradeMeteor;
              skillData.IncreaseRadius(upgradeMeteor.radius);
            }
          }


          break;

        case UpgradeStats.Chaos:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == UpgradeElements.Earth)
            {
              UpgradeEarth upgradeEarth = upgradeSkillConfig as UpgradeEarth;
              skillData.SetNewReduceChaosValue(upgradeEarth.reduceChaosPercentage);
            }
          }

          break;
        case UpgradeStats.Slow:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == UpgradeElements.Tornado)
            {
              UpgradeTornado upgradeTornado = upgradeSkillConfig as UpgradeTornado;
              skillData.SetNewSlowValue(upgradeTornado.slowPercentage, upgradeTornado.slowDuration);
            }
          }

          break;
        case UpgradeStats.Cooldown:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == upgradeSkillConfig.nameElement)
            {

              UpgradeCooldown upgradeCooldown = upgradeSkillConfig as UpgradeCooldown;
              skillData.SetNewCooldownValue(upgradeCooldown.cooldownPercentage);
            }
          }

          break;
        case UpgradeStats.Duration:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == upgradeSkillConfig.nameElement)
            {

              UpgradeDuration upgradeDuration = upgradeSkillConfig as UpgradeDuration;
              skillData.IncreaseDuration(upgradeDuration.durationValue);
            }
          }

          break;
      }

      upgradeController.RemoveUpgradeConfig(upgradeSkillConfig);
      OnAddUpgrade?.Invoke(upgradeSkillConfig.upgradeSkillUIConfig);
    }
  }
}