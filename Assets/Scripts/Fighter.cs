using UnityEngine;

public class Fighter : MonoBehaviour
{
  public int hitPoint = 10;
  public int maxHitpoint = 10;
  public float pushRecovery = 0.2f;

  protected float immuneTime = 1.0f;
  protected float lastImune;

  protected Vector3 pushDirection;

  protected virtual void ReceiveDamage(Damage dmg)
  {
    if (Time.time - lastImune > immuneTime)
    {
      lastImune = Time.time;
      hitPoint -= dmg.damageAmount;
      pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

      GameManager.instance.ShowText(dmg.damageAmount.ToString(), 20, Color.red, transform.position, Vector3.up * 50, 1f);

      if (hitPoint < 0)
      {
        hitPoint = 0;
        Death();
      }
    }
  }
  protected virtual void Death()
  {

  }
}
