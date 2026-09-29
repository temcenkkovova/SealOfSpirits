using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
  public Image imageField;
  private SkillData data;
  public TMP_Text numberSkillField;

  void Start()
  {
    imageField.color = Color.gray3;
  }
  public void Init(SkillData data, int idNumber)
  {
    this.data = data;
    imageField.sprite = data.icon;
    numberSkillField.text = idNumber.ToString();
    this.data.OnActivatedSkill += ChangeActiveState;
  }

  public void ChangeActiveState()
  {
    imageField.color = Color.white;
  }

  void OnDisable()
  {
    data.OnActivatedSkill -= ChangeActiveState;
  }
}