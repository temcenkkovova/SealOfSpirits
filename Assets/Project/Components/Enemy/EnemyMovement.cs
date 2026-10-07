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
  private float stopDistance = 2f;
  private Vector3 newAngelDirection = new Vector3();
  public bool isMoving;
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
      direction.y = 0f;
      if (direction.sqrMagnitude <= stopDistance * stopDistance)
      {
        isMoving = false;
        return;
      }
      isMoving = true;
      FollowTarget(targetTr.position);
    }


  }
  private bool CanMoveTo(Vector3 nextPosition)
  {
    foreach (EnemyMovement other in activeEnemies)
    {
      if (other == this) continue;

      Vector3 distanceBetweenOther = transform.position - other.transform.position;
      Vector3 nextDistanceBetweenOther = nextPosition - other.transform.position;


      distanceBetweenOther.y = 0f;
      nextDistanceBetweenOther.y = 0f;

      float minDistance = separationRadius + other.separationRadius;


      if (nextDistanceBetweenOther.sqrMagnitude < minDistance * minDistance &&
          nextDistanceBetweenOther.sqrMagnitude < distanceBetweenOther.sqrMagnitude)
      {
        return false;
      }
    }

    return true;
  }
  public void FollowTarget(Vector3 targetPosition)
  {
    if (IsKnockingBack) return;
    Vector3 nextPosition = transform.position;
    Vector3 forwardPosition = Vector3.MoveTowards(
       transform.position,
        targetPosition,
        moveSpeed * Time.deltaTime);

    if (CanMoveTo(forwardPosition))
    {
      nextPosition = forwardPosition;
    }
    else
    {
      nextPosition = TryMoveInDirection(targetPosition);
    }
    nextPosition.y = 0f;
    Vector3 moveDir = nextPosition - transform.position;
    Quaternion rotateTarget = Quaternion.LookRotation(moveDir);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotateTarget, 7f * Time.deltaTime);
    transform.position = nextPosition;
  }

  public Vector3 TryMoveInDirection(Vector3 targetPosition)
  {
    Vector3 stepPosition = transform.position;
    Vector3 direction = targetPosition - transform.position;
    direction.y = 0f;
    direction = direction.normalized;

    newAngelDirection = TryBypass(30, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }
    newAngelDirection = TryBypass(-30, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }

    newAngelDirection = TryBypass(60, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }
    newAngelDirection = TryBypass(-60, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }
    newAngelDirection = TryBypass(90, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }
    newAngelDirection = TryBypass(-90, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }
    newAngelDirection = TryBypass(-120, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }
    newAngelDirection = TryBypass(120, direction);
    if (CanMoveTo(newAngelDirection))
    {
      stepPosition = newAngelDirection;
      return stepPosition;
    }

    return stepPosition;
  }
  public Vector3 TryBypass(float angle, Vector3 direction)
  {
    Quaternion rotateAngle = Quaternion.AngleAxis(angle, Vector3.up);
    Vector3 rotateDirection = rotateAngle * direction;
    Vector3 tryStepPosition = transform.position + rotateDirection * moveSpeed * Time.deltaTime;
    return tryStepPosition;
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