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
      Debug.Log("Granted " + coinsReward + " coins");
    }
  }
}
