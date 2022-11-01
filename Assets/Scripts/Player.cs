using UnityEngine;

public class Player : Fighter
{
  private BoxCollider2D boxCollider;
  private Vector3 moveDelta;
  private RaycastHit2D hit;

  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
  }

  private void FixedUpdate()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");
    moveDelta = new Vector3(x, y, 0f);
    if (moveDelta.x > 0f)
    {
      transform.localScale = Vector3.one;
    }
    else if (moveDelta.x < 0f)
    {
      transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, new Vector2(0f, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    if (hit.collider == null)
    {
      transform.Translate(0f, moveDelta.y * Time.deltaTime, 0f);
    }

    hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, new Vector2(moveDelta.x, 0f), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    if (hit.collider == null)
    {
      transform.Translate(moveDelta.x * Time.deltaTime, 0f, 0f);
    }

  }
}
