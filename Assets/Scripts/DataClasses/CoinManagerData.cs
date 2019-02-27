
[System.Serializable]
public class CoinManagerData {

    public float totalMoney;
    public float totalMoneyPerSec;
    public float multiplier;

    public CoinManagerData(float totalMoney, float totalMoneyPerSec, float multiplier) {
        this.totalMoney = totalMoney;
        this.totalMoneyPerSec = totalMoneyPerSec;
        this.multiplier = multiplier;
    }

}