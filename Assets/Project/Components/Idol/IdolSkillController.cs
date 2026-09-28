using UnityEngine;

public class IdolSkillController : MonoBehaviour, IInteractable
{
  private IdolSkillConfig skillConfig;
  public Transform skillPosition;
  private float timer;


  public void Init(IdolSkillConfig idolSkillConfig)
  {
    skillConfig = idolSkillConfig;
  }
  public void Interact()
  {
    if (timer > 0)
    {
      Debug.Log("Cooldown skill");
      return;
    }
    else
    {
      timer = skillConfig.cooldown;
      IdolSkill skill = Instantiate(skillConfig.idolSkillPrefab);
      skill.Initialize(skillConfig);
    }

  }

  void Update()
  {
    if (timer > 0f) timer -= Time.deltaTime;
  }
}