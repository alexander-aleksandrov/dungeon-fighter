using UnityEngine;

public class Weapon : Collidable
{
  public int[] damageAmount = { 1, 2, 3, 4, 5 };
  public float[] pushForce = { 2f, 2.2f, 2.4f, 2.5f, 2.6f };
  public int weaponLevel = 0;
  private SpriteRenderer spriteRenderer;

  private float cooldown = 0.5f;
  private float lastSwing;
  private Animator animator;

  public void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  protected override void Start()
  {
    base.Start();
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
        damageAmount = damageAmount[weaponLevel],
        pushForce = pushForce[weaponLevel],
        origin = transform.position
      };

      coll.SendMessage("ReceiveDamage", dmg);
    }
  }
  private void Swing()
  {
    animator.SetTrigger("Swing");
  }

  public void UpgradeWeapon()
  {
    weaponLevel++;
    spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
  }

  public void SetWeaponLevel(int level)
  {
    weaponLevel = level;
    spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
  }

}
