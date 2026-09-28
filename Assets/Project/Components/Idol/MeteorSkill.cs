using Unity.VisualScripting;
using UnityEngine;

public class MeteorSkill : IdolSkill
{

  private IdolSkillConfig idolSkillConfig;
  private IdolSkillView idolSkillView;
  public LayerMask damageMask;

  private float currentZ;
  private float currentX;
  private bool IsOnGround = false;
  public override void Initialize(IdolSkillConfig idolSkillConfig, IdolSkillView idolSkillView)
  {
    this.idolSkillConfig = idolSkillConfig;
    this.idolSkillView = idolSkillView;
  }

  void Start()
  {
    IsOnGround = false;
    if (idolSkillView != null)
      idolSkillView.PlayWarning();

    currentZ = transform.position.z;
    currentX = transform.position.x;
  }

  void Update()
  {
    if (IsOnGround) return;
    transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentX, 0f, currentZ), (idolSkillConfig.duration - 1.5f) * Time.deltaTime);
  }

  void OnTriggerEnter(Collider other)
  {

    if (IsOnGround) return;
    if (other.gameObject.tag == "Ground")
    {
      IsOnGround = true;
      Explosion();
    }
  }
  public void Explosion()
  {

    Collider[] hits = Physics.OverlapSphere(transform.position, 10f, damageMask);

    foreach (Collider hit in hits)
    {
      IDamageable damageable = hit.GetComponent<IDamageable>();
      if (damageable == null) return;
      damageable.TakeDamage(idolSkillConfig.damage);
    }
    Destroy(gameObject, 2f);
  }



}