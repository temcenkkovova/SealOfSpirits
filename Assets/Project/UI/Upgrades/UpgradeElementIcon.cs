using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElementIcon : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  public Vector3 direction;
  public event Action OnArrived;
  private bool isInit = false;
  private Sequence moveSequence;
  private bool isStartAnimation = false;
  private bool isArrivedOnPosition = false;
  public void Init(Sprite icon, Vector3 direction)
  {
    spriteRenderer.sprite = icon;
    this.direction = direction;
    isInit = true;
  }

  void Update()
  {
    if (isInit)
    {
      transform.position = Vector3.MoveTowards(transform.position, direction, 30f * Time.deltaTime);
      Vector3 distance = transform.position - direction;
      distance.y = 0f;
      if (distance.sqrMagnitude <= 0.010 && !isArrivedOnPosition)
      {
        isArrivedOnPosition = true;
        Debug.Log("Upgrade icon arrived");
        OnArrived?.Invoke();
        Destroy(gameObject, 0.5f);
      }
      if (isStartAnimation) return;

      moveSequence?.Kill();

      moveSequence = DOTween.Sequence();
      moveSequence.Insert(0f, spriteRenderer.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 2f)).SetEase(Ease.OutQuad);
      moveSequence.SetLink(gameObject);
      isStartAnimation = true;
    }

  }

  void OnDestroy()
  {

    moveSequence?.Kill();
  }
}