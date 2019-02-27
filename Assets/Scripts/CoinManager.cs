using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {

    public CropsManager cropsManager = null;
    public TextMeshProUGUI coinText = null;
    public TextMeshProUGUI coinPerSecText = null;
    public TextMeshProUGUI multiplierText = null;

    public float totalMoney = 10;
    private float totalMoneyPerSec = 0;
    public float multiplier = 1; // can become array of multipliers


    // Save
    public CoinManagerData SaveData() {
        return new CoinManagerData(totalMoney, totalMoneyPerSec, multiplier);
    }

    // Load
    public void LoadData(CoinManagerData coinManagerData, DateTime lastSaveTime) {
        this.totalMoney = coinManagerData.totalMoney;
        this.totalMoneyPerSec = coinManagerData.totalMoneyPerSec;
        this.multiplier = coinManagerData.multiplier;

        OfflineIncome(lastSaveTime);
    }

    void Update() {
        if (totalMoneyPerSec > 0) {
            totalMoney += multiplier * totalMoneyPerSec * Time.deltaTime;
        }

        coinText.text = NumberFormatter.format(totalMoney, 40) + "(c)";
        coinPerSecText.text = NumberFormatter.format(totalMoneyPerSec * multiplier, 25) + "(c)/sec";
        if (multiplier > 1.1) {
            multiplierText.text = "Coin Multiplier: " + multiplier.ToString("F1");
        }
    }

    public void CropClicked(float price, float coinIncrease, bool first) {
        totalMoney -= price;
        totalMoneyPerSec += coinIncrease;
        if (first) {
            cropsManager.UnlockNextCrop();
        }
    }

    public void rewardMultiplier() {
        multiplier *= 1.5f;
        multiplierText.text = "Coin Multiplier: " + multiplier.ToString("F1");
    }

    private void OfflineIncome(DateTime lastSaveTime) {
        TimeSpan timeSpan = DateTime.UtcNow - lastSaveTime;
        float income = (float) (0.1 * totalMoneyPerSec * timeSpan.TotalSeconds);
        Debug.Log("Income" + income);
    }
}