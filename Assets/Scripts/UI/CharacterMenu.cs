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
    int weaponLevel = GameManager.instance.weapon.weaponLevel;
    weaponSprite.sprite = GameManager.instance.weaponSprites[weaponLevel];

    if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponsPrices.Count)
      upgradeWeaponButtonText.text = "MAX";
    else
      upgradeWeaponButtonText.text = GameManager.instance.weaponsPrices[weaponLevel].ToString();

    coinsMenuTxt.text = GameManager.instance.playerCoinsAmount.ToString();
    healthMenuTxt.text = GameManager.instance.player.hitPoint.ToString();
    levelText.text = GameManager.instance.GetCurrentLevel().ToString();

    xpBarTxt.text = "Not implemented";
    xpBar.localScale = new Vector3(0.5f, 0f, 0f);
  }
}
