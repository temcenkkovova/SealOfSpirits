using UnityEngine;

public class IdolBootstrap : MonoBehaviour
{

  public IdolConfig idolConfig;
  private IdolSkillController idolSkillController;

  void Awake()
  {
    idolSkillController = GetComponent<IdolSkillController>();

  }
  void Start()
  {
    if (idolSkillController != null)
      idolSkillController.Init(idolConfig.idolSkillConfig);
  }
}