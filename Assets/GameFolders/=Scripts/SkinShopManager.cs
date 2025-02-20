using UnityEngine;
using UnityEngine.UI;

public class SkinShopManager : MonoBehaviour
{
    public Text coinText;
    public SkinButton[] skinButtons;

    private int totalCoins;
    private int selectedSkinIndex;

    private MenuOptions _menuOptions;

    [SerializeField] private GameObject[] _LivesPrice;
    [SerializeField] private GameObject[] _LivesText;

    void Start()
    {
        CheckDefaultSkin();
        LoadSkins();
        UpdateCoinDisplay();

        _menuOptions = GetComponent<MenuOptions>();

        CheckLifes(0);
        CheckLifes(1);
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

    public void Buy1Life()
    {
        BuyLife(450, 0);
    }

    public void Buy3Lifes()
    {
        BuyLife(1100, 1);
    }

    private void BuyLife(int price, int index)
    {
        int status = PlayerPrefs.GetInt($"Life_{index}_Purchased", 0);
        if (status == 0)
        {
            int livePrice = price;
            if (totalCoins >= livePrice)
            {
                totalCoins -= livePrice;
                PlayerPrefs.SetInt("TotalCoins", totalCoins);
                PlayerPrefs.SetInt($"Life_{index}_Purchased", 1);
                MakeLivesBought(index);
                UpdateCoinDisplay();
                _menuOptions.PlayBuySound();
            }
            else
            {
                _menuOptions.PlayFailSound();
            }
        }
        
    }

    private void MakeLivesBought(int index)
    {
        _LivesPrice[index].SetActive(false);
        _LivesText[index].SetActive(true);
    }

    private void CheckLifes(int index)
    {
        int status = PlayerPrefs.GetInt($"Life_{index}_Purchased", 0);
        if (status == 1) MakeLivesBought(index);
    }
}