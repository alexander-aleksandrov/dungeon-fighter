using UnityEngine;

public class Player : Mover
{
  private SpriteRenderer _spriteRenderer;

  protected override void Start()
  {
    base.Start();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }
  public void SwapSprite(int currentCarachterSelection)
  {
    _spriteRenderer.sprite = GameManager.instance.playerSprites[currentCarachterSelection];
  }

  private void FixedUpdate()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");
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
}
