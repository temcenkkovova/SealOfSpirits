using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class ArriveOrbEffect : MonoBehaviour
{
  public Image arrivalGlow;
  private Sequence arrivalSequence;
  private Vector3 initialScale;


  private void Awake()
  {
    initialScale = arrivalGlow.rectTransform.localScale;
    ResetVisual();
  }

  public void Play()
  {
    if (!isActiveAndEnabled) return;
    arrivalSequence?.Kill(); // Every time when i call this method need to Kill old sequence;
    ResetVisual();
    RectTransform glowTr = arrivalGlow.rectTransform;

    glowTr.localScale = initialScale * 0.8f;

    arrivalSequence = DOTween.Sequence();

    arrivalSequence.Append(arrivalGlow.DOFade(0.9f, 0.4f));
    arrivalSequence.Append(arrivalGlow.DOFade(0f, 0.16f).SetEase(Ease.InQuad));
    arrivalSequence.Insert(
           0f,
           glowTr.DOScale(initialScale * 1.3f, 0.2f)
               .SetEase(Ease.OutQuad)
       );

    arrivalSequence.SetLink(gameObject);
  }
  private void ResetVisual()
  {
    Color color = arrivalGlow.color;
    color.a = 0f;
    arrivalGlow.color = color;

    arrivalGlow.rectTransform.localScale = initialScale;
  }
  private void OnDisable()
  {
    arrivalSequence?.Kill();

    if (arrivalGlow != null)
      ResetVisual();
  }
}