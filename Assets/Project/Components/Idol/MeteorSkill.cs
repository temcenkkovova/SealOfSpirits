using Unity.VisualScripting;
using UnityEngine;

public class MeteorSkill : IdolSkill
{

  private SkillData skillData;
  public IdolSkillView warningSkill;
  public LayerMask damageMask;



  private MeteorCollision meteorCollision;
  public override void Initialize(SkillData skillData)
  {
    this.skillData = skillData;

  }

  void Awake()
  {
    meteorCollision = GetComponentInChildren<MeteorCollision>();
  }

  void Start()
  {

    warningSkill.Init(skillData.Duration, skillData.Radius);
    meteorCollision.Init(skillData.Duration);

    if (warningSkill != null)
      warningSkill.PlayWarning();



    if (meteorCollision != null)
    {
      meteorCollision.OnGround += Explosion;
    }
  }



  public void Explosion()
  {
    Vector3 vector = new Vector3(transform.position.x, 0f, transform.position.z);
    Collider[] hits = Physics.OverlapSphere(vector, skillData.Radius, damageMask);

    foreach (Collider hit in hits)
    {
      IDamageable damageable = hit.GetComponent<IDamageable>();
      if (damageable == null) return;
      damageable.TakeDamage(skillData.Damage);
    }
    Destroy(gameObject, 2f);
  }

  void OnDisable()
  {
    if (meteorCollision != null)
    {
      meteorCollision.OnGround -= Explosion;
    }
  }

}