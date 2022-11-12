using System.Collections;
using UnityEngine;

public class NPCText : Collidable
{
  public string msg;
  public bool isTalking = false;
  protected override void OnCollide(Collider2D coll)
  {
    if (isTalking)
    {
      return;
    }
    else
    {
      StartCoroutine("Talk");
      isTalking = true;
    }

  }

  private IEnumerator Talk()
  {
    GameManager.instance.ShowText(
      msg, 25, Color.white,
      transform.position + new Vector3(0, 0.16f, 0),
      Vector3.zero, 3f);
    yield return new WaitForSeconds(4);
    isTalking = false;
  }
}
