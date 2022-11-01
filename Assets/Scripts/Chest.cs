using UnityEngine;

public class Chest : Collectable
{
  public Sprite emptyChest;
  public int coinsReward = 5;
  protected override void OnCollect()
  {
    if (collected == false)
    {
      collected = true;
      GetComponent<SpriteRenderer>().sprite = emptyChest;

      string msg = "+" + coinsReward + " coins";
      GameManager.instance.ShowText(msg, 25, Color.yellow, transform.position, Vector3.up * 50, 3f);
      Debug.Log("Granted coins");
      GameManager.instance.coins += coinsReward;
    }
  }
}
