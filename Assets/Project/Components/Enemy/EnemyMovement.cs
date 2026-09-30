using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  private float moveSpeed;
  private Transform targetTr;
  private EnemyStatus enemyStatus;

  void Awake()
  {
    enemyStatus = GetComponent<EnemyStatus>();
  }
  public void Init(Transform targetTr, float moveSpeed)
  {
    this.moveSpeed = moveSpeed;
    this.targetTr = targetTr;
  }


  void Update()
  {
    if (enemyStatus.IsStunned) return;
    Vector3 direction = targetTr.position - transform.position;
    if (direction.sqrMagnitude <= 0.01f) return;
    FollowTarget(targetTr.position);

  }

  public void FollowTarget(Vector3 dir)
  {
    transform.position = Vector3.MoveTowards(transform.position, dir, moveSpeed * Time.deltaTime);
  }
}