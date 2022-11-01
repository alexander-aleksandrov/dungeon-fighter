using UnityEngine;

public class Weapon : Collidable
{
  public int damageAmount = 1;
  public float pushForce = 2f;
  public int weaponLevel = 1;
  private SpriteRenderer spriteRenderer;

  private float cooldown = 0.5f;
  private float lastSwing;
  private Animator animator;

  protected override void Start()
  {
    base.Start();
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
  }

  protected override void Update()
  {
    base.Update();
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (Time.time - lastSwing > cooldown)
      {
        lastSwing = Time.time;
        Swing();
      }
    }
  }

  protected override void OnCollide(Collider2D coll)
  {
    if (coll.tag == "Fighter")
    {
      if (coll.name == "Player")
        return;

      Damage dmg = new Damage()
      {
        damageAmount = damageAmount,
        pushForce = pushForce,
        origin = transform.position
      };

      coll.SendMessage("ReceiveDamage", dmg);
    }
  }
  private void Swing()
  {
    animator.SetTrigger("Swing");
  }

}
