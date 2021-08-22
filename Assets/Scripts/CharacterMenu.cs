using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text Fields
    public Text levelText, hitpointText, goldText, upgradeCostText, expText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform expBar;

    // Character Selection
    // If I click the Right arrow, boolean = true, If I click the Left arrow, boolean = false
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            // If we went too far
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            // If we went too far
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update Character Information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        goldText.text = GameManager.instance.gold.ToString();

        // Exp Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if (currentLevel == GameManager.instance.expTable.Count)
        {
            expText.text = GameManager.instance.experience.ToString() + " total experience points"; // Display total exp if we reached MAX Level
            expBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelExp = GameManager.instance.GetExpToLevel(currentLevel - 1);
            int currentLevelExp = GameManager.instance.GetExpToLevel(currentLevel);

            int diff = currentLevelExp - prevLevelExp;
            int currentExpToLevel = GameManager.instance.experience - prevLevelExp;

            float completionRatio = (float) currentExpToLevel / (float) diff;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            expText.text = currentExpToLevel.ToString() + " / " + diff;
        }
    }
}
