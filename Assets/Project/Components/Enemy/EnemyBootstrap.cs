using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
  private SealComponent target;
  private EnemyMovement enemyMovement;
  private EnemyHealth enemyHealth;
  private EnemyConfig enemyConfig;



  void Start()
  {

    enemyMovement.Init(target.transform, enemyConfig.moveSpeed);
    enemyHealth.Init(enemyConfig.health);
  }
  public void Init(EnemyConfig enemyConfig, SealComponent target)
  {
    this.enemyConfig = enemyConfig;
    this.target = target;
  }

  void Awake()
  {
    enemyMovement = GetComponent<EnemyMovement>();
    enemyHealth = GetComponent<EnemyHealth>();
  }

}