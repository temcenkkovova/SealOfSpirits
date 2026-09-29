using UnityEngine;

public class PlayerSkillsUI : MonoBehaviour
{
  public Transform gridParent;
  public SkillUI skillPrefabUI;
  public PlayerSkills playerSkills;

  void Start()
  {
    if (playerSkills == null) return;
    playerSkills.OnSkillsInitialized += ShowSkillsUI;
  }

  public void ShowSkillsUI()
  {
    int number = 1;
    foreach (SkillData skill in playerSkills.skillsData)
    {
      SkillUI skillUI = Instantiate(skillPrefabUI, gridParent);
      skillUI.Init(skill, number);
      number++;
    }
  }

  void OnDisable()
  {
    if (playerSkills == null) return;
    playerSkills.OnSkillsInitialized -= ShowSkillsUI;
  }
}