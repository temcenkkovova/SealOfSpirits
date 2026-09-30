

using UnityEngine;

public class WaveSkill : IdolSkill
{

  private SkillData skillData;
  private Vector3 targetSkillPos;

  public override void Initialize(SkillData skillData, Vector3 skillPos)
  {
    this.skillData = skillData;
    targetSkillPos = skillPos;
    targetSkillPos.y = transform.position.y;
    Vector3 direction = targetSkillPos - transform.position;
    if (direction.sqrMagnitude > 0.0001f)
    {
      transform.rotation = Quaternion.LookRotation(
          direction,
          Vector3.up
      );
    }
  }

  void Update()
  {

    transform.position = Vector3.MoveTowards(transform.position, targetSkillPos, 3f * Time.deltaTime);

    if ((targetSkillPos - transform.position).sqrMagnitude <= 0.01f)
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Enemy")
    {
      Debug.Log("Enemy water");
    }
  }
}