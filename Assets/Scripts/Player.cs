using UnityEngine;

public class Player : Mover
{
  private SpriteRenderer _spriteRenderer;
  public bool isAlive = true;

  protected override void Start()
  {
    base.Start();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }
  protected override void ReceiveDamage(Damage dmg)
  {
    if (!isAlive)
      return;
    base.ReceiveDamage(dmg);
    GameManager.instance.OnHitPointChange();
  }
  protected override void Death()
  {
    GameManager.instance.deathMenuAnimator.SetTrigger("show");

  }
  public void SwapSprite(int currentCarachterSelection)
  {
    _spriteRenderer.sprite = GameManager.instance.playerSprites[currentCarachterSelection];
  }

  private void FixedUpdate()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");
    if (isAlive)
      UpdateMotor(new Vector3(x, y));
  }

  public void OnLevelUp()
  {
    maxHitpoint++;
    hitPoint = maxHitpoint;
  }

  public void SetLevel(int level)
  {
    for (int i = 0; i < level; i++)
    {
      OnLevelUp();
    }
  }

  public void Heal(int healingAmountPerSec)
  {
    if (hitPoint == maxHitpoint)
      return;

    hitPoint += healingAmountPerSec;

    GameManager.instance.ShowText("+" + healingAmountPerSec.ToString() + "Hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
    GameManager.instance.OnHitPointChange();
  }

  public void Respawn()
  {
    Heal(maxHitpoint);
    isAlive = true;
    lastImune = Time.time;
    pushDirection = Vector3.zero;
  }
}
