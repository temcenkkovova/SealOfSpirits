using System;
using System.Collections;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IStunable
{
  public bool IsStunned { get; private set; } = false;
  public event Action<bool> OnStunStatusChanged;

  public void Stunned(float duration)
  {
    if (IsStunned) return;
    IsStunned = true;
    StartCoroutine(stunDurationTimer(duration));
    OnStunStatusChanged?.Invoke(IsStunned);
  }

  public void ChangeStunnedStatus(bool newStatus)
  {
    IsStunned = newStatus;
    OnStunStatusChanged?.Invoke(IsStunned);
  }

  IEnumerator stunDurationTimer(float duration)
  {
    yield return new WaitForSeconds(duration);
    ChangeStunnedStatus(false);
  }
}