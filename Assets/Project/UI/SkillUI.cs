using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
  public Image imageField;
  private SkillData data;
  public GameObject cooldownPanel;
  public TMP_Text cooldownField;

  private Vector3 initialScale;
  private Sequence cooldownSequence;


  void OnEnable()
  {
    imageField.color = Color.gray3;
    cooldownPanel.SetActive(false);
  }
  void Start()
  {
    initialScale = imageField.transform.localScale;
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
    cooldownSequence?.Kill();

    imageField.transform.localScale = initialScale;
    cooldownSequence = DOTween.Sequence();

    cooldownSequence.Insert(0f, imageField.DOFade(0.9f, 0.4f));

    cooldownSequence.Insert(0f, imageField.transform.DOScale(initialScale * 1.3f, 0.3f)).SetEase(Ease.OutQuad);
    cooldownSequence.Insert(0.3f, imageField.transform.DOScale(initialScale, 0.5f)).SetEase(Ease.OutQuad);
    cooldownSequence.SetLink(gameObject);
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
    cooldownSequence?.Kill();
  }
}