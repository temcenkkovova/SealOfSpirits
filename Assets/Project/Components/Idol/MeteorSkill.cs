using Unity.VisualScripting;
using UnityEngine;

public class MeteorSkill : IdolSkill
{

  private SkillData skillData;
  public IdolSkillView warningSkill;
  public LayerMask damageMask;
  public ParticleSystem rainParticle;
  private AudioSource audioSource;
  private bool isStartExplosion = false;
  private MeteorCollision meteorCollision;

  private float timer;
  public override void Initialize(SkillData skillData, Vector3 skillPos)
  {
    this.skillData = skillData;

  }

  void Awake()
  {
    //meteorCollision = GetComponentInChildren<MeteorCollision>();
    audioSource = GetComponent<AudioSource>();
  }

  void Start()
  {

    warningSkill.Init(skillData.Duration, skillData.Radius);
    //meteorCollision.Init(skillData.Duration);

    if (warningSkill != null)
      warningSkill.PlayWarning();
    warningSkill.OnWarningFinish += StartSkillExplosion;



    if (meteorCollision != null)
    {
      // meteorCollision.OnGround += Explosion;
    }
  }

  void Update()
  {
    if (!isStartExplosion) return;

    timer += Time.deltaTime;
    Explosion();
    if (timer == 3f)
    {
      isStartExplosion = false;
      rainParticle.Stop();
      audioSource.Stop();
    }



  }

  public void StartSkillExplosion()
  {
    rainParticle.Play();
    audioSource.Play();
    isStartExplosion = true;
  }
  public void Explosion()
  {
    Vector3 vector = new Vector3(transform.position.x, 0f, transform.position.z);
    Collider[] hits = Physics.OverlapSphere(vector, 2 * skillData.Radius, damageMask);

    foreach (Collider hit in hits)
    {
      IDamageable damageable = hit.GetComponent<IDamageable>();

      if (damageable == null) continue;
      damageable.TakeDamage(skillData.Damage);
    }
    Destroy(gameObject, 3.2f);
  }

  void OnDisable()
  {
    if (meteorCollision != null)
    {
      //meteorCollision.OnGround -= Explosion;
    }
  }

}