using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

  private Button button;
  private AudioSource audioSource;
  public Image backgroundImage;
  private Sequence hoverSequence;


  void Awake()
  {
    button = GetComponent<Button>();
    audioSource = GetComponent<AudioSource>();

  }
  public void OnPointerEnter(PointerEventData eventData)
  {
    if (isActiveAndEnabled)
    {

      hoverSequence?.Kill();
      hoverSequence = DOTween.Sequence();
      hoverSequence.Insert(0f, backgroundImage.DOFade(0.2f, 0.5f));
      hoverSequence.SetLink(gameObject);

      audioSource.Play();

    }
  }
  public void OnPointerExit(PointerEventData eventData)
  {
    if (isActiveAndEnabled)
    {
      hoverSequence?.Kill();
      hoverSequence = DOTween.Sequence();
      hoverSequence.Insert(0f, backgroundImage.DOFade(0, 0.5f));
      hoverSequence.SetLink(gameObject);

    }
  }


}