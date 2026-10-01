using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
  private float moveSpeed;
  private Transform targetTr;
  private EnemyStatus enemyStatus;
  public bool IsKnockingBack { get; private set; }
  private float knockbackDuration;
  private float knockbackSpeed;
  private Vector3 knockbackDirection;
  private static readonly List<EnemyMovement> activeEnemies = new();
  private float separationRadius = 0.4f;

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
  private bool CanMoveTo(Vector3 nextPosition)
  {
    foreach (EnemyMovement other in activeEnemies)
    {
      if (other == this) continue;

      Vector3 currentOffset = transform.position - other.transform.position;
      Vector3 nextOffset = nextPosition - other.transform.position;


      currentOffset.y = 0f;
      nextOffset.y = 0f;

      float minDistance = separationRadius + other.separationRadius;


      if (nextOffset.sqrMagnitude < minDistance * minDistance &&
          nextOffset.sqrMagnitude < currentOffset.sqrMagnitude)
      {
        return false;
      }
    }

    return true;
  }
  public void FollowTarget(Vector3 targetPosition)
  {
    if (IsKnockingBack) return;

    Vector3 nextPosition = Vector3.MoveTowards(
        transform.position,
        targetPosition,
        moveSpeed * Time.deltaTime);

    if (CanMoveTo(nextPosition))
    {
      transform.position = nextPosition;
    }
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


  private void OnEnable()
  {
    activeEnemies.Add(this);
  }

  private void OnDisable()
  {
    activeEnemies.Remove(this);
  }
}