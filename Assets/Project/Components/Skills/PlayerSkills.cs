using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
  public List<IdolSkillConfig> skillConfigs = new List<IdolSkillConfig>();
  public List<SkillData> skillsData = new List<SkillData>();

  void Start()
  {
    foreach (IdolSkillConfig skill in skillConfigs)
    {
      skillsData.Add(new SkillData(skill));
    }
  }

  public void UseSkill(int skillNumber)
  {
    SkillData currentUseSkill = skillsData[skillNumber];
    if (currentUseSkill.IsActivated)
    {
      Debug.Log("Use skill");
    }
    else
    {
      Debug.Log("Is not learned");
    }
  }

  public void HandleActivatedSkill(int skillNumber)
  {
    SkillData skill = skillsData[skillNumber];
    if (skill.IsActivated) return;
    skill.ActivateSkill();
  }
}