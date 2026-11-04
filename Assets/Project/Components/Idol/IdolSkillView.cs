using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class IdolSkillView : MonoBehaviour
{
  public Transform fill;
  public Transform ring;
  private Vector3 fillFinalScale;
  private Sequence warningSequence;
  private float duration;
  private float radius;
  public event Action OnWarningFinish;
  private SkillData skillData;
  void Awake()
  {

  }

  public void Init(SkillData skillData)
  {
    this.skillData = skillData;

    fillFinalScale = fill.localScale * (skillData.Radius - 0.2f);
    ring.localScale = ring.localScale * skillData.Radius;
  }
  void Start()
  {

  }
  public void PlayWarning()
  {
    warningSequence.Kill();
    fill.gameObject.SetActive(true);
    ring.gameObject.SetActive(true);
    fill.localScale = Vector3.zero;
    warningSequence = DOTween.Sequence();
    warningSequence.Append(fill.DOScale(fillFinalScale, 2f).SetEase(Ease.Linear));

    warningSequence.OnComplete(() =>
    {
      OnWarningFinish?.Invoke();
      //fill.gameObject.SetActive(false);
      //ring.gameObject.SetActive(false);
    });

  }

  void OnDestroy()
  {
    warningSequence.Kill();
  }
}