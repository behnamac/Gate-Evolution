using Elementary.Scripts.Data.Management;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private SOMoneyData moneyData;

    private void Awake()
    {
        HandleLoadData();
    }

    private void Start()
    {
     
    }

    private void SetCoin(int value)
    {
        moneyData.Money = value;
        // Set TotalCoin;
        coinText.text = moneyData.Money.ToString();
        SaveData();
    }


    private void HandleLoadData()
    {
        var loadedData = DataManager.GetWithJson<SOMoneyData>();
        if (loadedData == null)
        {
            moneyData = new SOMoneyData()
            {
                Money = 0,
            };

            SaveData();
        }
        else
            moneyData = loadedData;
        coinText.text = moneyData.Money.ToString();

    }

    private void SaveData()
    {
        DataManager.SaveWithJson(moneyData);
    }

}
