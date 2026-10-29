

using UnityEngine;

public class WaveSkill : IdolSkill
{

  private SkillData skillData;
  private Vector3 targetSkillPos;
  private float knockSpeed = 4f;
  private float skillSpeed = 10f;
  public bool IsSlowUpgrade { get; private set; }
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

    transform.position = Vector3.MoveTowards(transform.position, targetSkillPos, skillSpeed * Time.deltaTime);

    if ((targetSkillPos - transform.position).sqrMagnitude <= 0.01f)
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Enemy")
    {

      other.GetComponent<EnemyMovement>().KnockBackFromWave(2f, knockSpeed, transform.forward);

    }
  }

  public void HandleSlowUpgrade()
  {
    IsSlowUpgrade = true;
  }
}