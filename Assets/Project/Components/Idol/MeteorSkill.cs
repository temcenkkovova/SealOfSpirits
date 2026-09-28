using Unity.VisualScripting;
using UnityEngine;

public class MeteorSkill : IdolSkill
{

  private IdolSkillConfig idolSkillConfig;
  private IdolSkillView idolSkillView;

  private float currentZ;
  private float currentX;
  public override void Initialize(IdolSkillConfig idolSkillConfig, IdolSkillView idolSkillView)
  {
    this.idolSkillConfig = idolSkillConfig;
    this.idolSkillView = idolSkillView;
  }

  void Start()
  {
    if (idolSkillView != null)
      idolSkillView.PlayWarning();

    currentZ = transform.position.z;
    currentX = transform.position.x;
  }

  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentX, 0f, currentZ), 2f * Time.deltaTime);
  }
}