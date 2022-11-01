using UnityEngine;

public abstract class Mover : Fighter
{
  protected BoxCollider2D boxCollider;
  protected Vector3 moveDelta;
  protected RaycastHit2D hit;
  protected float xSpeed = 1f;
  protected float ySpeed = 0.75f;

  protected virtual void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
  }


  protected virtual void UpdateMotor(Vector3 input)
  {
    moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed);
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
