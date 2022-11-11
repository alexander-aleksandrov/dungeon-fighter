using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
  //Text fields in menu UI
  public TextMeshProUGUI levelText, coinsMenuTxt, healthMenuTxt, upgradeWeaponButtonText, xpBarTxt;

  //Logic fields 
  private int currentCarachterSelection = 0;
  public Image characterSelectionSprite;
  public Image weaponSprite;
  public RectTransform xpBar;

  //Character Selection
  public void OnArrowClick(bool rightArrow)
  {
    int playerSpritesCount = GameManager.instance.playerSprites.Count;

    if (rightArrow)
    {
      currentCarachterSelection++;
      if (playerSpritesCount == currentCarachterSelection)
        currentCarachterSelection = 0;

      OnSelectionChanged();
    }
    else
    {
      currentCarachterSelection--;
      if (currentCarachterSelection < 0)
      {
        currentCarachterSelection = playerSpritesCount - 1;
      }
      OnSelectionChanged();
    }

  }
  private void OnSelectionChanged()
  {
    characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCarachterSelection];
    GameManager.instance.player.SwapSprite(currentCarachterSelection);
    GameManager.instance.currentCharacterSpriteIndx = currentCarachterSelection;
  }

  //Weapon Upgrade
  public void OnUpgradeWeaponClick()
  {
    if (GameManager.instance.TryUpgradeWeapon())
    {
      UpdateMenu();
    }
  }
  public void UpdateMenu()
  {
    int currentLevel = GameManager.instance.GetCurrentLevel();
    int weaponLevel = GameManager.instance.weapon.weaponLevel;


    weaponSprite.sprite = GameManager.instance.weaponSprites[weaponLevel];

    if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponsPrices.Count)
      upgradeWeaponButtonText.text = "MAX";
    else
      upgradeWeaponButtonText.text = GameManager.instance.weaponsPrices[weaponLevel].ToString();

    GameManager instance = GameManager.instance;
    coinsMenuTxt.text = instance.playerCoinsAmount.ToString();
    healthMenuTxt.text = GameManager.instance.player.hitPoint.ToString();


    levelText.text = currentLevel.ToString();

    // xpBar
    if (currentLevel == GameManager.instance.xpTable.Count)
    {
      xpBarTxt.text = GameManager.instance.playerXpAmount.ToString() + " total xp";
      xpBar.localScale = Vector3.one;
    }
    else
    {
      int currentLevelXp = GameManager.instance.GetXPToLevelUP(currentLevel);
      int prevLevelXp = GameManager.instance.GetXPToLevelUP(currentLevel - 1);
      int currentXPIntoLevel = GameManager.instance.playerXpAmount - prevLevelXp;

      int diff = currentLevelXp - prevLevelXp;
      float completionRatio = (float)currentXPIntoLevel / (float)diff;
      xpBar.localScale = new Vector3(completionRatio, 1f, 1f);
      xpBarTxt.text = currentXPIntoLevel.ToString() + " / " + diff.ToString();
    }
  }
}
