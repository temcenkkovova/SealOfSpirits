using System.Collections;
using System.Collections.Generic;
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
  private static readonly List<EnemyMovement> activeEnemies = new();
  private float separationRadius = 0.4f;
  private float stopDistance = 3.5f;
  private Vector3 newAngelDirection = new Vector3();
  public bool isMoving;
  private float initialMoveSpeed;

  private float slowDuration;
  private float slowPercentage;
  public bool IsSlowing { get; private set; }
  private Coroutine slowCoroutine;
  private EnemyCollision enemyCollision;
  void Awake()
  {
    enemyStatus = GetComponent<EnemyStatus>();
    enemyCollision = GetComponent<EnemyCollision>();
  }
  public void Init(Transform targetTr, float moveSpeed)
  {
    this.moveSpeed = moveSpeed;
    this.targetTr = targetTr;
    initialMoveSpeed = moveSpeed;
  }


  void Update()
  {

    if (IsKnockingBack)
    {
      if (!enemyCollision.IsCollisionWithWall)
      {
        KnockBackMovement();
        isMoving = false;

      }
      else
      {
        IsKnockingBack = false;
      }

    }
    else
    {
      if (enemyStatus.IsStunned)
      {
        isMoving = false;
        return;
      }
      Vector3 direction = targetTr.position - transform.position;
      direction.y = 0f;
      isMoving = direction.sqrMagnitude > stopDistance * stopDistance;
      if (direction.sqrMagnitude <= stopDistance * stopDistance)
      {
        return;
      }

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

    moveDir.y = 0f;
    if (moveDir.sqrMagnitude > 0.000001f)
    {

      Quaternion rotateTarget = Quaternion.LookRotation(moveDir);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotateTarget, 7f * Time.deltaTime);
    }
    isMoving = moveDir.sqrMagnitude > 0.000001f;



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
    isMoving = false;
    transform.position +=
        knockbackDirection * knockbackSpeed * stepTime;

    knockbackDuration -= stepTime;

    if (knockbackDuration <= 0f)
    {
      IsKnockingBack = false;
      if (IsSlowing && slowCoroutine == null)
      {
        moveSpeed *= slowPercentage;
        slowCoroutine = StartCoroutine(slowSpeedCoroutine());
      }
    }

  }

  IEnumerator slowSpeedCoroutine()
  {
    yield return new WaitForSeconds(slowDuration);
    moveSpeed = initialMoveSpeed;
    IsSlowing = false;
    slowCoroutine = null;
  }

  public void InitSlowMovement(float duration, float slowPercentage)
  {
    slowDuration = duration;
    this.slowPercentage = slowPercentage;
    IsSlowing = true;
  }
  private void OnEnable()
  {
    activeEnemies.Add(this);
  }

  private void OnDisable()
  {
    activeEnemies.Remove(this);
    if (slowCoroutine != null)
      StopCoroutine(slowCoroutine);
  }
}