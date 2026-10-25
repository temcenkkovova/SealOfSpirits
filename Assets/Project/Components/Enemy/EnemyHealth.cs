using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

  public event Action<EnemyHealth> OnDead;
  public float maxHealth;
  public bool isDead { get; private set; }


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

      Destroy(gameObject, 0.2f);
    }
  }
}