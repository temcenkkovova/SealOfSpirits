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

  public event Action<int> OnSkillActivated;
  private int currentNumberLearned = 0;


  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    skillAimingComponent = GetComponent<SkillAimingComponent>();
  }

  void Update()
  {
    if (skillsData.Count > 0)
    {
      foreach (SkillData skill in skillsData)
      {
        skill.ResetCooldown();
      }
    }
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
    if (currentUseSkill.CanUseSkill)
    {
      if (currentUseSkill.name == "Water")
      {
        Vector3 skillPosition = new Vector3(castSkillPosition.x, 0f, castSkillPosition.z);
        IdolSkill idolSkill = Instantiate(currentUseSkill.SkillPrefab, transform.position, currentUseSkill.SkillPrefab.transform.rotation);
        idolSkill.Initialize(currentUseSkill, skillPosition);
      }
      else
      {
        Vector3 skillPosition = new Vector3(castSkillPosition.x, 0f, castSkillPosition.z);
        IdolSkill idolSkill = Instantiate(currentUseSkill.SkillPrefab, skillPosition, currentUseSkill.SkillPrefab.transform.rotation);
        idolSkill.Initialize(currentUseSkill, Vector3.zero);
      }
      currentUseSkill.SetCooldownTimer();
    }
    else
    {
      Debug.Log("Skill is reloading");
    }


  }

  public void TryUseSkill(int skillNumber)
  {

    if (skillsData[skillNumber].IsActivated)
    {
      currentUseSkill = skillsData[skillNumber];
      if (currentUseSkill.name == "Rock")
      {
        CastSkill(transform.position);
      }
      if (currentUseSkill.name == "Meteor")
      {
        skillAimingComponent.StartAimingMeteor(currentUseSkill.Radius);
      }
      if (currentUseSkill.name == "Water")
      {
        skillAimingComponent.StartAimingWave(currentUseSkill.Radius);
      }



    }
    else
    {
      Debug.Log("Is not learned");
    }
  }

  public void HandleActivatedSkill(int skillNumber)
  {
    if (skillNumber > currentNumberLearned) return;
    SkillData skill = skillsData[skillNumber];
    if (skill.IsActivated) return;

    skill.ActivateSkill();
    OnSkillActivated?.Invoke(skillNumber);
  }
  public void ActivateAllSkills()
  {
    foreach (SkillData skill in skillsData)
    {
      skill.ActivateSkill();
    }
  }
  public void ChangeNumberLearned()
  {
    currentNumberLearned++;
  }

  void OnDisable()
  {
    if (playerInputController == null) return;

    playerInputController.OnSkillPressed -= TryUseSkill;
    skillAimingComponent.OnChoosePosition -= CastSkill;

  }
}