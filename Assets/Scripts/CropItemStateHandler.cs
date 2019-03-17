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
    public Button cropClickButton;
    public ProgressBar progressBar;

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
        Button[] buttons = this.GetComponentsInChildren<Button>();

        buyButton = buttons[0];
        buyButton.onClick.AddListener(OnBuyButtonClick);

        progressBar = this.GetComponentInChildren<ProgressBar>();
        cropClickButton = buttons[1];
        cropClickButton.onClick.AddListener(OnCropClicked);

        price = crop.initialValue * 10;
        amount = 0;
        coinIncrease = crop.initialValue;
        coinTime = crop.baseTime;

        Text[] texts = this.GetComponentsInChildren<Text>();
        // Set crop name
        texts[0].text = crop.cropName;

        // Set button text
        refreshBarInfo();
    }

    void OnBuyButtonClick() {
        if (coinManager.totalMoney >= price) {
            if (amount == 0) {
                coinManager.UnlockNextCrop();
            }
            price *= PRICE_INCREASE_MULTIPLIER;
            amount++;
            refreshBarInfo();
        }
    }

    void OnCropClicked() {
        if (amount > 0 && progressBar != null && !progressBar.running) {
            progressBar.Execute(coinTime, ProgressBarFinished);
        }
    }

    void ProgressBarFinished() {
        coinManager.CropSingleIncome(amount * coinIncrease);
    }

    void refreshBarInfo() {
        // todo change to button.getTextMEshPro Component -> set text
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "COST:\n"+NumberFormatter.format(price);
        Text[] texts = this.GetComponentsInChildren<Text>();
        texts[1].text = "Amount: " + amount;
        // texts[2].text = "Next: +" + NumberFormatter.format(coinIncrease) + "(c)/sec";
    }

}