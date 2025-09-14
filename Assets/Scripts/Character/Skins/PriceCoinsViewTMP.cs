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
    private bool inited;

    public void Initialize(CoinService service, int targetPrice)
    {
        if (inited && coinService != null)
            coinService.OnCoinsChanged -= UpdateView;

        coinService = service;
        price = targetPrice;

        if (coinService != null)
        {
            coinService.OnCoinsChanged += UpdateView;
            inited = true;
        }
        UpdateView();
    }

    private void OnDestroy()
    {
        if (inited && coinService != null)
            coinService.OnCoinsChanged -= UpdateView;
    }

    private void UpdateView()
    {
        if (priceText == null) return;
        priceText.text = price.ToString();
        priceText.color = (coinService != null && coinService.CoinCount >= price) ? affordableColor : notAffordableColor;
    }
}
