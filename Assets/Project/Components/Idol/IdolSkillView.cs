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
  void Awake()
  {
    fillFinalScale = fill.localScale;
  }

  public void Init(float duration)
  {
    this.duration = duration;
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
    warningSequence.Append(fill.DOScale(fillFinalScale, duration).SetEase(Ease.Linear));

    warningSequence.OnComplete(() =>
    {
      fill.gameObject.SetActive(false);
      ring.gameObject.SetActive(false);
    });

  }

  void OnDestroy()
  {
    warningSequence.Kill();
  }
}