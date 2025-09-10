using Services.CoinService;
using TMPro;
using UnityEngine;

public class PriceCoinsViewTMP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Color affordableColor = Color.white;
    [SerializeField] private Color notAffordableColor = Color.red;

    private CoinService coinService;
    private int price;

    public void Initialize(CoinService service, int targetPrice)
    {
        coinService = service;
        price = targetPrice;
        coinService.OnCoinsChanged += UpdateView;
        UpdateView();
    }

    void OnDestroy()
    {
        if (coinService != null) coinService.OnCoinsChanged -= UpdateView;
    }

    private void UpdateView()
    {
        priceText.text = price.ToString();
        if (coinService.CoinCount >= price)
            priceText.color = affordableColor;
        else
            priceText.color = notAffordableColor;
    }
}