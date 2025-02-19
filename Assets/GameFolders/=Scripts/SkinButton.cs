using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkinButton
{
    public GameObject priceObject;
    public Text equipText;
    public int price;

    public Button button;
    public Sprite defaultSprite;
    public Sprite selectedSprite;
}