using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public StoreItem storeItem;

    private Button button;
    private StoreHandler storeHandler;

    private Image image;
    private Color canBuyColor;
    private Color boughtColor;
    private Color cantAffordColor;

    private int coins;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        image = GetComponent<Image>();
        canBuyColor = new Color(1, 1, 1, 60f / 255f); // 1 1 1 60/255
        boughtColor = new Color(0, 206f / 255f, 1, 60f / 255f); // 0 206/255 1 60/255
        cantAffordColor = new Color(30f / 255f, 30f / 255f, 30f / 255f, 60f / 255f); // 30/255 30/255 30/255 60/255

        storeHandler = GameObject.Find("Store Manager").GetComponent<StoreHandler>();
        button.onClick.AddListener(() =>
        {
            int coins = PlayerPrefs.GetInt("coins", 0);
            if (coins >= storeItem.price) { 
                storeItem.purchased = true;
                UpdateButton(storeItem);
                PlayerPrefs.SetInt("coins", coins - storeItem.price);
                GameObject.Find("Canvas").GetComponent<MainMenuManager>().coinsText.text = "Coins: " + PlayerPrefs.GetInt("coins", 0);
                this.coins = PlayerPrefs.GetInt("coins", 0);
                storeHandler.AddObjectToPurchasedSet(storeItem);
            }
        });
    }

    private void OnEnable()
    {
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (storeItem.purchased) {
            button.interactable = false;
            image.color = boughtColor;
        } 
        else if (coins >= storeItem.price) {
            button.interactable = true;
            image.color = canBuyColor;
        }
        else if (coins < storeItem.price) {
            button.interactable = false;
            image.color = cantAffordColor;
        }

    }

    public void UpdateButton(StoreItem storeItem) {
        this.storeItem = storeItem;

        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = storeItem.itemName;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = storeItem.itemDescription;
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Price: " + storeItem.price;
    }
}
