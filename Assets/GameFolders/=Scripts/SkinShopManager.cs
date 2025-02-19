using UnityEngine;
using UnityEngine.UI;

public class SkinShopManager : MonoBehaviour
{
    public Text coinText;
    public SkinButton[] skinButtons;

    private int totalCoins;
    private int selectedSkinIndex;

    private MenuOptions _menuOptions;

    void Start()
    {
        CheckDefaultSkin();
        LoadSkins();
        UpdateCoinDisplay();

        _menuOptions = GetComponent<MenuOptions>();
    }

    public void BuyOrEquipSkin(int skinIndex)
    {
        if (PlayerPrefs.GetInt($"Skin_{skinIndex}_Purchased", 0) == 1)
        {
            EquipSkin(skinIndex);
        }
        else
        {
            int skinPrice = skinButtons[skinIndex].price;
            if (totalCoins >= skinPrice)
            {
                totalCoins -= skinPrice;
                PlayerPrefs.SetInt("TotalCoins", totalCoins);
                PlayerPrefs.SetInt($"Skin_{skinIndex}_Purchased", 1);
                EquipSkin(skinIndex);
                UpdateCoinDisplay();
                _menuOptions.PlayBuySound();
            }
            else
            {
                _menuOptions.PlayFailSound();
            }
        }
    }

    private void EquipSkin(int skinIndex)
    {
        _menuOptions.PlayClickSound();
        selectedSkinIndex = skinIndex;
        PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);

        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt($"Skin_{i}_Purchased", 0) == 1;
            skinButtons[i].priceObject.SetActive(!isPurchased);
            skinButtons[i].equipText.gameObject.SetActive(isPurchased);
            skinButtons[i].equipText.text = isPurchased ? (i == selectedSkinIndex ? "EQUIPPED" : "EQUIP") : "";

            if (skinButtons[i].button != null)
            {
                skinButtons[i].button.image.sprite = (i == selectedSkinIndex) ? skinButtons[i].selectedSprite : skinButtons[i].defaultSprite;
            }
        }

        PlayerPrefs.Save();
    }

    private void LoadSkins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);

        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt($"Skin_{i}_Purchased", 0) == 1;
            skinButtons[i].priceObject.SetActive(!isPurchased);
            skinButtons[i].equipText.gameObject.SetActive(isPurchased);
            skinButtons[i].equipText.text = isPurchased ? (i == selectedSkinIndex ? "EQUIPPED" : "EQUIP") : "";

            if (skinButtons[i].button != null)
            {
                skinButtons[i].button.image.sprite = (i == selectedSkinIndex) ? skinButtons[i].selectedSprite : skinButtons[i].defaultSprite;
            }
        }
    }

    private void UpdateCoinDisplay()
    {
        coinText.text = totalCoins.ToString();
    }

    private void CheckDefaultSkin()
    {
        if (!PlayerPrefs.HasKey("Skin_0_Purchased"))
        {
            PlayerPrefs.SetInt("Skin_0_Purchased", 1);
            PlayerPrefs.SetInt("SelectedSkin", 0);
            PlayerPrefs.Save();
        }
    }
}