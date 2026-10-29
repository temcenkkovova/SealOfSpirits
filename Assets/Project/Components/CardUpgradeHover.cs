using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardUpgradeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

  private Button button;
  private Sequence hoverSequence;
  private Vector3 initialScale;

  void Awake()
  {
    button = GetComponent<Button>();

  }

  void Start()
  {
    initialScale = transform.localScale;
  }
  public void OnPointerEnter(PointerEventData eventData)
  {
    if (isActiveAndEnabled)
    {
      Debug.Log(button.name);

      hoverSequence?.Kill();

      hoverSequence = DOTween.Sequence();
      hoverSequence.SetUpdate(true);
      hoverSequence.Insert(0f, button.transform.DOScale(initialScale * 1.1f, 0.3f)).SetEase(Ease.OutQuad); ;
      hoverSequence.SetLink(gameObject);


    }
  }
  public void OnPointerExit(PointerEventData eventData)
  {
    if (isActiveAndEnabled)
    {

      hoverSequence?.Kill();

      hoverSequence = DOTween.Sequence();
      hoverSequence.SetUpdate(true);
      hoverSequence.Insert(0f, button.transform.DOScale(1f, 0.3f)).SetEase(Ease.OutQuad); ;
      hoverSequence.SetLink(gameObject);
    }
  }
}