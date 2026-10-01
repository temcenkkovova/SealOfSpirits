using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

  public event Action OnDead;
  public float maxHealth;


  public void Init(float health)
  {
    maxHealth = health;
  }
  public void TakeDamage(float damage)
  {
    maxHealth -= damage;
    if (maxHealth <= 0f)
    {
      OnDead?.Invoke();
      Destroy(gameObject, 0.5f);
    }
  }
}