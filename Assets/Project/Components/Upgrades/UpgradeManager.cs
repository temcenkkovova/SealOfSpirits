using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
  public List<UpgradeSkillConfig> currentUpgrades = new List<UpgradeSkillConfig>();
  public PlayerSkills playerSkills;

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

            if (skillData.name == "Meteor")
            {
              UpgradeMeteor upgradeMeteor = upgradeSkillConfig as UpgradeMeteor;
              skillData.IncreaseRadius(upgradeMeteor.radius);
            }
          }

          Debug.Log("Radius");
          return;

        case UpgradeStats.Chaos:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == "Rock")
            {
              UpgradeEarth upgradeEarth = upgradeSkillConfig as UpgradeEarth;
              skillData.SetNewReduceChaosValue(upgradeEarth.reduceChaosPercentage);
            }
          }
          Debug.Log("Chaos");
          return;
        case UpgradeStats.Slow:
          foreach (SkillData skillData in playerSkills.skillsData)
          {

            if (skillData.name == "Water")
            {
              UpgradeTornado upgradeTornado = upgradeSkillConfig as UpgradeTornado;
              skillData.SetNewSlowValue(upgradeTornado.slowPercentage, upgradeTornado.slowDuration);
            }
          }
          Debug.Log("SLOw");
          return;
      }
    }
  }
}