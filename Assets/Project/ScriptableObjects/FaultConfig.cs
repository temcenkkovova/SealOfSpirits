using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fault")]
public class FaultConfig : ScriptableObject
{
  public List<EnemyConfig> enemyConfigs;

  public int spawnDelay;

}