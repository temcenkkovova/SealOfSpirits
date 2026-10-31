using System;
using UnityEngine;

public class SkillData
{

  public float Damage { get; private set; }
  public float Cooldown { get; private set; }
  public float Radius { get; private set; }
  public float Duration { get; private set; }
  public IdolSkill SkillPrefab { get; private set; }
  public bool IsActivated { get; private set; } = false;
  public float cooldownTimer;
  private bool startedTimer = false;
  public bool CanUseSkill { get; private set; }
  public Sprite icon;
  public UpgradeElements name;
  public event Action OnActivatedSkill;

  public event Action<float> OnChangeTimer;
  public event Action OnStartedTimer;
  public event Action OnFinishedTimer;
  public bool IsReduceChaosActive { get; private set; } = false;
  public float ReduceChaosValue { get; private set; }
  public bool IsSlowMovement { get; private set; } = false;
  public float SlowMovementValue { get; private set; }
  public float SlowDuration { get; private set; }
  public SkillData(IdolSkillConfig skillConfig)
  {
    Damage = skillConfig.damage;
    Cooldown = skillConfig.cooldown;
    Radius = skillConfig.radius;
    Duration = skillConfig.duration;
    SkillPrefab = skillConfig.idolSkillPrefab;
    icon = skillConfig.icon;
    name = skillConfig.nameSkill;

  }

  public void IncreaseRadius(float value)
  {
    Radius *= 1f + value / 100f;
  }
  public void SetNewReduceChaosValue(float newValue)
  {
    IsReduceChaosActive = true;
    ReduceChaosValue = newValue;
  }

  public void SetNewCooldownValue(float newValue)
  {
    Cooldown *= 1f - newValue / 100f;
  }
  public void SetNewSlowValue(float newValue, float slowDuration)
  {
    IsSlowMovement = true;
    SlowMovementValue = newValue;
    SlowDuration = slowDuration;
  }

  public void IncreaseDuration(float value)
  {
    Duration += value;
  }
  public void ActivateSkill()
  {

    IsActivated = true;
    OnActivatedSkill?.Invoke();
  }
  public void SetCooldownTimer()
  {

    cooldownTimer = Cooldown;
    startedTimer = true;
    OnStartedTimer?.Invoke();
  }
  public void ResetCooldown()
  {
    if (cooldownTimer > 0)
    {
      CanUseSkill = false;
      cooldownTimer -= Time.deltaTime;
      OnChangeTimer?.Invoke(cooldownTimer);
    }

    if (cooldownTimer <= 0 && startedTimer)
    {
      startedTimer = false;
      CanUseSkill = true;
      OnFinishedTimer?.Invoke();
    }
    if (cooldownTimer <= 0 && !startedTimer)
    {
      CanUseSkill = true;
    }


  }
}