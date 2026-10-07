using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
  public Animator animator;

  private EnemyMovement enemyMovement;
  private EnemyHealth enemyHealth;

  void Awake()
  {
    enemyMovement = GetComponent<EnemyMovement>();
    enemyHealth = GetComponent<EnemyHealth>();
  }

  void Update()
  {
    if (enemyMovement == null) return;
    animator.SetBool("Moving", enemyMovement.isMoving);
  }

  void Start()
  {
    if (enemyHealth == null) return;
    enemyHealth.OnDead += HandleDieAnimation;
  }
  public void HandleDieAnimation(EnemyHealth enemyHealth)
  {
    animator.SetTrigger("Dead");
  }

  void OnDisable()
  {
    if (enemyHealth == null) return;
    enemyHealth.OnDead -= HandleDieAnimation;
  }
}