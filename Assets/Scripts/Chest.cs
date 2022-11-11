using UnityEngine;

public class Chest : Collectable
{
  public Sprite emptyChest;
  public int coinsReward = 25;
  protected override void OnCollect()
  {
    if (collected == false)
    {
      collected = true;
      GetComponent<SpriteRenderer>().sprite = emptyChest;

      string msg = "+" + coinsReward + " coins";
      GameManager.instance.ShowText(msg, 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
      GameManager.instance.playerCoinsAmount += coinsReward;
    }
  }
}
