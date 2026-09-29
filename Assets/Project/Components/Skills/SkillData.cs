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
  public event Action OnActivatedSkill;
  public SkillData(IdolSkillConfig skillConfig)
  {
    Damage = skillConfig.damage;
    Cooldown = skillConfig.cooldown;
    Radius = skillConfig.radius;
    Duration = skillConfig.duration;
    SkillPrefab = skillConfig.idolSkillPrefab;
  }

  public void ActivateSkill()
  {
    Debug.Log("skill is activated");
    IsActivated = true;
    OnActivatedSkill?.Invoke();
  }
}