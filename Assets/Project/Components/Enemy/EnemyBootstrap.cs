using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
  private PlayerBootstrap target;
  private EnemyMovement enemyMovement;
  private float moveSpeed = 3f;

  public PlayerBootstrap playerBootstrap;

  void Start()
  {
    Init(playerBootstrap);
    enemyMovement.Init(target.transform, moveSpeed);
  }
  public void Init(PlayerBootstrap target)
  {
    this.target = target;
  }

  void Awake()
  {
    enemyMovement = GetComponent<EnemyMovement>();
  }

}