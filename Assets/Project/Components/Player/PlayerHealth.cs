using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
  public void TakeDamage(float damage)
  {
    Debug.Log("Got damage" + damage);
  }
}