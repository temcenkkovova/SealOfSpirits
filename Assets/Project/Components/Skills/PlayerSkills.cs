using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
  public List<IdolSkillConfig> skillConfigs = new List<IdolSkillConfig>();
  public List<SkillData> skillsData = new List<SkillData>();
  public event Action OnSkillsInitialized;
  private PlayerInputController playerInputController;
  private SkillAimingComponent skillAimingComponent;
  private SkillData currentUseSkill;


  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    skillAimingComponent = GetComponent<SkillAimingComponent>();
  }
  void Start()
  {
    foreach (IdolSkillConfig skill in skillConfigs)
    {
      skillsData.Add(new SkillData(skill));
    }
    OnSkillsInitialized?.Invoke();

    if (playerInputController != null)
    {
      playerInputController.OnSkillPressed += TryUseSkill;
    }


    skillAimingComponent.OnChoosePosition += CastSkill;
  }

  public void CastSkill(Vector3 castSkillPosition)
  {
    Vector3 skillPosition = new Vector3(castSkillPosition.x, 4f, castSkillPosition.z);
    IdolSkill idolSkill = Instantiate(currentUseSkill.SkillPrefab, skillPosition, currentUseSkill.SkillPrefab.transform.rotation);
    idolSkill.Initialize(currentUseSkill);
  }
  public void TryUseSkill(int skillNumber)
  {

    if (skillsData[skillNumber].IsActivated)
    {
      currentUseSkill = skillsData[skillNumber];
      skillAimingComponent.StartAiming(currentUseSkill.Radius);

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

  void OnDisable()
  {
    if (playerInputController == null) return;

    playerInputController.OnSkillPressed -= TryUseSkill;
    skillAimingComponent.OnChoosePosition -= CastSkill;

  }
}