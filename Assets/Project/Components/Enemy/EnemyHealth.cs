using System;
using DG.Tweening;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

  public event Action<EnemyHealth> OnDead;
  public float maxHealth;
  public bool isDead { get; private set; }
  private Vector3 scaleVector = new Vector3(0, 0, 0);
  private SoulToSealEffect soulToSealEffect;

  void Awake()
  {
    soulToSealEffect = FindFirstObjectByType<SoulToSealEffect>();
  }
  public void Init(float health)
  {
    maxHealth = health;

  }
  public void TakeDamage(float damage)
  {
    maxHealth -= damage;
    if (maxHealth <= 0f && !isDead)
    {
      isDead = true;
      OnDead?.Invoke(this);

      transform.DOScale(scaleVector, 0.3f).OnComplete(() =>
      {
        Destroy(gameObject);

        Vector3 orbPosition = new Vector3(transform.position.x, 2f, transform.position.z);
        soulToSealEffect.CreateOrbEffect(orbPosition);

      });



    }
  }
}