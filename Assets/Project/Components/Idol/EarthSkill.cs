public class EarthSkill : IdolSkill
{

  private IdolSkillConfig idolSkillConfig;
  private IdolSkillView idolSkillView;
  public override void Initialize(IdolSkillConfig idolSkillConfig, IdolSkillView idolSkillView)
  {
    this.idolSkillConfig = idolSkillConfig;
    this.idolSkillView = idolSkillView;
  }
}