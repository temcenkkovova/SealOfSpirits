using UnityEngine;
[CreateAssetMenu(menuName = "Enemy")]
public class EnemyConfig : ScriptableObject
{
  public EnemyBootstrap enemyPrefab;

  public float health;
  public float moveSpeed;
  public float shieldValue;
  public float sealAmountReward;
  public float chaosAmount;
}