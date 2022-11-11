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
      return;
    }
    instance = this;
    SceneManager.sceneLoaded += LoadState;
    DontDestroyOnLoad(gameObject);
  }
  public List<Sprite> playerSprites;
  public List<Sprite> weaponSprites;
  public List<int> weaponsPrices;
  public List<int> xpTable;

  public Player player;
  public Weapon weapon;
  public FloatingTextManager floatingTextManager;

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

  public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
  {
    floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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

    s.Append("0" + "|");
    s.Append(playerCoinsAmount.ToString() + "|");
    s.Append(playerXpAmount.ToString() + "|");
    s.Append(weapon.weaponLevel.ToString());

    PlayerPrefs.SetString("SaveState", s.ToString());
    Debug.Log("State Saved");
  }
  public void LoadState(Scene scene, LoadSceneMode mode)
  {
    if (!PlayerPrefs.HasKey("SaveState"))
    {
      return;
    }
    string[] data = PlayerPrefs.GetString("SaveState").Split('|');
    //skin on data[0]
    playerCoinsAmount = int.Parse(data[1]);
    playerXpAmount = int.Parse(data[2]);
    weapon.SetWeaponLevel(int.Parse(data[3]));

    Debug.Log("State Loaded");
  }
}
