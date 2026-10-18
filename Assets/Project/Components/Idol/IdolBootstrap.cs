using UnityEngine;

public class IdolBootstrap : MonoBehaviour
{

  public IdolConfig idolConfig;
  private IdolSkillController idolSkillController;
  public GameObject tutorialTargetVisual;
  public int ownNumber;

  void Awake()
  {
    idolSkillController = GetComponent<IdolSkillController>();

  }
  void Start()
  {
    tutorialTargetVisual.SetActive(false);
    if (idolSkillController != null)
      idolSkillController.Init(idolConfig.idolSkillConfig);
  }

  public void ChangeTargetVisualStatus(int stageNumber)
  {
    if (ownNumber == stageNumber)
    {
      tutorialTargetVisual.SetActive(true);
    }
    else
    {
      tutorialTargetVisual.SetActive(false);
    }
  }
}