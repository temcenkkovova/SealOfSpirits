using UnityEngine;
public enum UpgradeStats
{
  Radius, Chaos, Slow, Cooldown, Duration
}

public enum UpgradeElements { Meteor, Tornado, Earth }
public class UpgradeSkillConfig : ScriptableObject
{
  public UpgradeSkillUIConfig upgradeSkillUIConfig;
  public UpgradeStats upgradeStat;
  public UpgradeElements nameElement;

}