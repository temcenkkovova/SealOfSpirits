using UnityEngine;

public class EarthSkill : IdolSkill
{

  private SkillData skillData;
  public IdolSkillView skillView;
  public LayerMask damageMask;
  private float stunDuration = 3f;
  public override void Initialize(SkillData skillData, Vector3 skillPos)
  {
    this.skillData = skillData;

  }
  void Start()
  {
    skillView.Init(skillData.Duration, skillData.Radius);
    if (skillView != null)
      skillView.PlayWarning();
    Stun();
  }

  public void Stun()
  {
    Vector3 vector = new Vector3(transform.position.x, 0f, transform.position.z);
    Collider[] hits = Physics.OverlapSphere(vector, skillData.Radius, damageMask);

    foreach (Collider hit in hits)
    {
      IStunable stunnable = hit.GetComponent<IStunable>();
      if (stunnable == null) continue;
      stunnable.Stunned(stunDuration);
    }
    Destroy(gameObject, 0.5f);
  }
}