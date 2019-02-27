using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropItemStateHandler : MonoBehaviour {

    private const float PRICE_INCREASE_MULTIPLIER = 1.5f;
    private const float COIN_INCREASE_MULTIPLIER = 1.1f;

    public Crop thisCrop = null;
    public CoinManager coinManager;
    public Button buyButton;


    // Crop Fields
    private float coinTime;
    private float price;
    public int amount;
    private float coinIncrease;

    public void LoadData(int amount) {
        if (amount != 0) {
            this.amount = amount;
            price *= (float) Math.Pow(PRICE_INCREASE_MULTIPLIER, amount - 1);
            coinIncrease *= (float) Math.Pow(COIN_INCREASE_MULTIPLIER, amount - 1);
        }
        refreshBarInfo();
    }


    public void Initialise(Crop crop, CoinManager coinManager) {

        // Set Class Fields
        this.thisCrop = crop;
        this.coinManager = coinManager;
        buyButton = this.GetComponentInChildren<Button>();
        buyButton.onClick.AddListener(OnBuyButtonClick);

        price = crop.initialValue * 10;
        amount = 0;
        coinIncrease = crop.initialValue;

        // set name
        Text[] texts = this.GetComponentsInChildren<Text>();
        texts[0].text = crop.cropName;

        // set button text
        refreshBarInfo();

        // Calculate stuffs
        coinTime = Mathf.Sqrt(crop.initialValue);
    }

    void OnBuyButtonClick() {
        if (coinManager.totalMoney >= price) {
            coinManager.CropClicked(price, coinIncrease, (amount == 0));
            price *= PRICE_INCREASE_MULTIPLIER;
            amount++;
            coinIncrease *= COIN_INCREASE_MULTIPLIER;
            refreshBarInfo();
        }
    }

    void refreshBarInfo() {
        // todo change to button.getTextMEshPro Component -> set text
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "COST:\n"+NumberFormatter.format(price);
        Text[] texts = this.GetComponentsInChildren<Text>();
        texts[1].text = "Amount: " + amount;
        texts[2].text = "Next: +" + NumberFormatter.format(coinIncrease) + "(c)/sec";
    }

    /*
     * State:
     *     NEW - (reduced text) - extra on click handler
     *     Normal - >=1 seconds to cash - coin values
     *     Fast - <1 seconds to cash - coinPerSecond values
     *
     * enable button if enough money
     */

    /*
     * Button click
     *     - Check current moneys (double check)
     *     - If first time - call unlock next item (Crop Class)
     *     - Increase coinnnnns ? Coin Manager
     */

}