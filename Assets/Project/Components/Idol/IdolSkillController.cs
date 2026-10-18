
using System;
using UnityEngine;

public class IdolSkillController : MonoBehaviour, IInteractable
{
  private IdolSkillConfig skillConfig;
  public int idSkill;
  public GameObject skillPosition;
  public IdolBootstrap idolBootstrap;

  public event Action<string> OnSetNewInteractVisual;
  private float timer;
  void Awake()
  {
    idolBootstrap = GetComponent<IdolBootstrap>();
  }

  public void Init(IdolSkillConfig idolSkillConfig)
  {
    skillConfig = idolSkillConfig;
  }
  public int Interact()
  {
    if (idolBootstrap.IsCurrentActive)
    {
      SetNewInteractVisual();
    }
    return idSkill;

  }
  public void SetNewInteractVisual()
  {
    OnSetNewInteractVisual?.Invoke("Press E");
  }
  public void ResetInteractVisual()
  {
    OnSetNewInteractVisual?.Invoke("Learn");
  }


  void Update()
  {
    if (timer > 0f) timer -= Time.deltaTime;
  }
}