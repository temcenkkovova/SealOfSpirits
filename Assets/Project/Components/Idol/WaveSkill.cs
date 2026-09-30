

using UnityEngine;

public class WaveSkill : IdolSkill
{

  private SkillData skillData;
  private Vector3 targetSkillPos;

  public override void Initialize(SkillData skillData, Vector3 skillPos)
  {
    this.skillData = skillData;
    targetSkillPos = skillPos;

  }

  void Update()
  {
    transform.rotation = Quaternion.LookRotation(targetSkillPos);
    transform.position = Vector3.MoveTowards(transform.position, targetSkillPos, 3f * Time.deltaTime);

    if ((targetSkillPos - transform.position).sqrMagnitude <= 0.01f)
    {
      Destroy(gameObject);
    }
  }
}