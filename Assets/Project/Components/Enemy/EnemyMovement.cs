using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  private float moveSpeed;
  private Transform targetTr;
  private EnemyStatus enemyStatus;
  public bool IsKnockingBack { get; private set; }
  private float knockbackDuration;
  private float knockbackSpeed;
  private Vector3 knockbackDirection;

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

    if (IsKnockingBack)
    {
      KnockBackMovement();
    }
    else
    {
      if (enemyStatus.IsStunned) return;
      Vector3 direction = targetTr.position - transform.position;
      if (direction.sqrMagnitude <= 0.01f) return;
      FollowTarget(targetTr.position);
    }


  }

  public void FollowTarget(Vector3 dir)
  {
    if (IsKnockingBack) return;
    transform.position = Vector3.MoveTowards(transform.position, dir, moveSpeed * Time.deltaTime);
  }

  public void KnockBackFromWave(float knockbackDuration, float knockbackSpeed, Vector3 knockbackDirection)
  {

    knockbackDirection.y = 0f;
    this.knockbackDuration = knockbackDuration;
    this.knockbackSpeed = knockbackSpeed;
    this.knockbackDirection = knockbackDirection.normalized;
    IsKnockingBack = knockbackDuration > 0f;
  }

  public void KnockBackMovement()
  {
    float stepTime = Mathf.Min(Time.deltaTime, knockbackDuration);

    transform.position +=
        knockbackDirection * knockbackSpeed * stepTime;

    knockbackDuration -= stepTime;

    if (knockbackDuration <= 0f)
    {
      IsKnockingBack = false;
    }
  }
}