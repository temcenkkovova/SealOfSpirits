using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
  public Image imageField;
  private SkillData data;
  public GameObject cooldownPanel;
  public TMP_Text cooldownField;


  void Start()
  {
    imageField.color = Color.gray3;
    HideCooldownPanel();
  }
  public void Init(SkillData data, int idNumber)
  {
    this.data = data;
    imageField.sprite = data.icon;

    this.data.OnActivatedSkill += ChangeActiveState;
    this.data.OnChangeTimer += ChangeTimer;
    this.data.OnStartedTimer += ShowCooldownPanel;
    this.data.OnFinishedTimer += HideCooldownPanel;
  }

  public void ChangeActiveState()
  {
    imageField.color = Color.white;
  }

  public void ShowCooldownPanel()
  {

    cooldownPanel.SetActive(true);
  }
  public void HideCooldownPanel()
  {
    cooldownPanel.SetActive(false);
  }
  public void ChangeTimer(float time)
  {
    cooldownField.text = time.ToString("F1");
  }

  void OnDisable()
  {
    data.OnActivatedSkill -= ChangeActiveState;
    data.OnChangeTimer -= ChangeTimer;
    data.OnStartedTimer -= ShowCooldownPanel;
    data.OnFinishedTimer -= HideCooldownPanel;
  }
}