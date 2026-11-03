using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
  private SealComponent target;
  private EnemyMovement enemyMovement;
  private EnemyHealth enemyHealth;
  public EnemyConfig enemyConfig { get; private set; }


  void Awake()
  {
    enemyMovement = GetComponent<EnemyMovement>();
    enemyHealth = GetComponent<EnemyHealth>();
  }
  void Start()
  {


    if (enemyHealth == null) return;
    enemyHealth.Init(enemyConfig.health);
    enemyHealth.OnDead += HandleSealReward;
  }
  public void Init(EnemyConfig enemyConfig, SealComponent target)
  {
    this.enemyConfig = enemyConfig;
    this.target = target;
    enemyMovement.Init(target.transform, enemyConfig.moveSpeed);
  }



  public void HandleSealReward(EnemyHealth enemyHealth)
  {
    target.AddProgressAmount(enemyConfig.sealAmountReward);
  }
  void OnDisable()
  {
    if (enemyHealth == null) return;
    enemyHealth.OnDead -= HandleSealReward;
  }

}