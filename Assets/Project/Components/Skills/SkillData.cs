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
  public bool CanUseSkill { get; private set; }
  public Sprite icon;
  public string name;
  public event Action OnActivatedSkill;

  public event Action<float> OnChangeTimer;
  public event Action OnStartedTimer;
  public event Action OnFinishedTimer;
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

  public void ActivateSkill()
  {

    IsActivated = true;
    OnActivatedSkill?.Invoke();
  }
  public void SetCooldownTimer()
  {

    cooldownTimer = Cooldown;
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
    else
    {
      CanUseSkill = true;
      OnFinishedTimer?.Invoke();
    }

  }
}