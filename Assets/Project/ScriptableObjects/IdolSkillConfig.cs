using UnityEngine;
[CreateAssetMenu(menuName = "Idol/Skill")]
public class IdolSkillConfig : ScriptableObject
{
  public IdolSkill idolSkillPrefab;
  public float damage;
  public float cooldown;
  public float radius;

}