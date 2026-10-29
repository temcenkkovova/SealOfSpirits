using UnityEngine;
public enum UpgradeStats
{
  Radius, Chaos, Slow
}
public class UpgradeSkillConfig : ScriptableObject
{
  public UpgradeSkillUIConfig upgradeSkillUIConfig;
  public UpgradeStats upgradeStat;

}