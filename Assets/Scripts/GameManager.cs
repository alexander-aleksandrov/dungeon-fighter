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
  public int coins;
  public int experience;

  public void SaveState()
  {
    var s = new StringBuilder();

    s.Append("0" + "|");
    s.Append(coins.ToString() + "|");
    s.Append(experience.ToString() + "|");
    s.Append("0");

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
    coins = int.Parse(data[1]);
    experience = int.Parse(data[2]);
    //weapon level

    Debug.Log("State Loaded");
  }
}
