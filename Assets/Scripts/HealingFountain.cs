using UnityEngine;

public class HealingFountain : Collidable
{
  public int healingAmountPerSec = 1;
  private float _healCooldown = 1.0f;
  private float _lastHeal;

  protected override void OnCollide(Collider2D coll)
  {
    if (coll.name != "Player")
      return;
    if (Time.time - _lastHeal > _healCooldown)
    {
      _lastHeal = Time.time;
      GameManager.instance.player.Heal(healingAmountPerSec);
    }
  }
}
