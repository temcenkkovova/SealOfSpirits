using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
  public TMP_Text elementIconTitle;
  public Image elemIcon;
  public TMP_Text upgradeTitle;
  public TMP_Text upgradeBonus;
  public TMP_Text description;
  public TMP_Text tip;
  public TMP_Text number;
  public Image background;

  public UpgradeSkillConfig config { get; private set; }


  public void Init(UpgradeSkillUIConfig configUI, UpgradeSkillConfig config)
  {
    elementIconTitle.text = configUI.elementIconTitle;
    elemIcon.sprite = configUI.elementIcon;
    elemIcon.color = configUI.elementColor;
    elementIconTitle.color = configUI.elementColor;
    upgradeBonus.text = configUI.bonusUpgrade;
    upgradeBonus.color = configUI.elementColor;
    upgradeTitle.text = configUI.titleUpgrade;
    description.text = configUI.description;
    tip.text = configUI.tip;
    background.sprite = configUI.background;
    number.text = "1";
    tip.color = configUI.elementColor;
    this.config = config;
  }
}