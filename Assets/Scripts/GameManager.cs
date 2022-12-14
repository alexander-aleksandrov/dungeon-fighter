using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  private void Awake()
  {
    if (GameManager.instance != null)
    {
      Destroy(gameObject);
      Destroy(player.gameObject);
      Destroy(floatingTextManager.gameObject);
      Destroy(hud.gameObject);
      Destroy(menu.gameObject);
      return;
    }
    instance = this;
    SceneManager.sceneLoaded += LoadState;
    SceneManager.sceneLoaded += OnsceneLoaded;
  }
  public List<Sprite> playerSprites;
  public List<Sprite> weaponSprites;
  public List<int> weaponsPrices;
  public List<int> xpTable;

  public Player player;
  public Weapon weapon;
  public FloatingTextManager floatingTextManager;
  public RectTransform hitPointBar;
  public GameObject hud;
  public GameObject menu;
  public Animator deathMenuAnimator;
  public int currentCharacterSpriteIndx;
  public int playerCoinsAmount;
  public int playerXpAmount;

  public bool TryUpgradeWeapon()
  {
    if (weaponsPrices.Count <= weapon.weaponLevel)
      return false;

    if (playerCoinsAmount >= weaponsPrices[weapon.weaponLevel])
    {
      playerCoinsAmount -= weaponsPrices[weapon.weaponLevel];
      weapon.UpgradeWeapon();
      return true;
    }
    return false;
  }

  //Hit point Bar
  public void OnHitPointChange()
  {
    float ratio = (float)player.hitPoint / (float)player.maxHitpoint;
    hitPointBar.localScale = new Vector3(1f, ratio, 1f);
  }

  //Xp system
  public int GetCurrentLevel()
  {
    int levelSumXp = 0;
    int level = 0;
    while (playerXpAmount >= levelSumXp)
    {
      levelSumXp += xpTable[level];
      level++;
      if (level == xpTable.Count)
        return level;
    }
    return level;
  }
  public int GetXPToLevelUP(int currentLevel)
  {
    int xpLeft = 0;
    int level = 0;
    while (level < currentLevel)
    {
      xpLeft += xpTable[level];
      level++;
    }
    return xpLeft;
  }
  public void GrantXP(int xp)
  {
    int currentLevel = GetCurrentLevel();
    playerXpAmount += xp;
    if (currentLevel < GetCurrentLevel())
    {
      OnLevelUp();
    }
  }

  private void OnLevelUp()
  {
    player.OnLevelUp();
    OnHitPointChange();
  }

  //Death menu
  public void Respawn()
  {
    deathMenuAnimator.SetTrigger("hide");
    SceneManager.LoadScene("Main");
    player.Respawn();
  }

  //Floating text
  public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
  {
    floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
  }

  //On scene loaded 
  public void OnsceneLoaded(Scene scene, LoadSceneMode mode)
  {
    player.transform.position = GameObject.Find("SpawnPoint").transform.position;
  }

  //Save state
  /*
   * int preferedPlayerSkin
   * int playerCoinsAmount
   * int playerXpAmount
   * int weaponLevel
   */
  public void SaveState()
  {
    var s = new StringBuilder();

    s.Append(currentCharacterSpriteIndx.ToString() + "|");
    s.Append(playerCoinsAmount.ToString() + "|");
    s.Append(playerXpAmount.ToString() + "|");
    s.Append(weapon.weaponLevel.ToString());

    PlayerPrefs.SetString("SaveState", s.ToString());
    Debug.Log("State Saved");
  }
  public void LoadState(Scene scene, LoadSceneMode mode)
  {
    SceneManager.sceneLoaded -= LoadState;
    if (!PlayerPrefs.HasKey("SaveState"))
    {
      return;
    }


    string[] data = PlayerPrefs.GetString("SaveState").Split('|');
    this.player.GetComponent<SpriteRenderer>().sprite = playerSprites[int.Parse(data[0])];
    playerCoinsAmount = int.Parse(data[1]);
    playerXpAmount = int.Parse(data[2]);
    if (GetCurrentLevel() != 1)
    {
      player.SetLevel(GetCurrentLevel());
    }
    weapon.SetWeaponLevel(int.Parse(data[3]));
  }
}
