using UnityEngine;

public class EnemyHitbox : Collidable
{
  public int damage;
  public float pushForce;

  protected override void OnCollide(Collider2D coll)
  {
    if (coll.name == "Player")
    {
      Damage dmg = new Damage()
      {
        damageAmount = damage,
        pushForce = pushForce,
        origin = transform.position
      };
      coll.SendMessage("ReceiveDamage", dmg);
    }
  }
}
