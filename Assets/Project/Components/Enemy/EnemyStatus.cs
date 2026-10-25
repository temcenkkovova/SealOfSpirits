using System;
using System.Collections;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IStunable
{
  public bool IsStunned { get; private set; } = false;
  public event Action<bool> OnStunStatusChanged;
  public bool IsInSealRadius { get; private set; } = false;
  public GameObject stunVisualParent;

  public void Stunned(float duration)
  {
    if (IsStunned) return;
    ChangeStunnedStatus(true);

    StartCoroutine(stunDurationTimer(duration));

    OnStunStatusChanged?.Invoke(IsStunned);
  }

  public void ChangeStunnedStatus(bool newStatus)
  {
    IsStunned = newStatus;
    stunVisualParent.SetActive(IsStunned);
    OnStunStatusChanged?.Invoke(IsStunned);
  }

  IEnumerator stunDurationTimer(float duration)
  {
    yield return new WaitForSeconds(duration);

    ChangeStunnedStatus(false);
  }

  public void ChangeSealRadiusStatus(bool newStatus)
  {
    IsInSealRadius = newStatus;
  }
}