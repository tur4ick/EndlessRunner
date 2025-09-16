using System;
using Services.CoinService;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Character.Skins
{
    public class PriceCoinsViewTMP : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Color _affordableColor = Color.white;
        [SerializeField] private Color _notAffordableColor = Color.red;

        [Inject] private CoinService _coinService;
        private int _price;
        
        private void Start()
        {
            _coinService.OnCoinsChanged += UpdateView;
        }

        public void Initialize(int targetPrice)
        {
            _price = targetPrice;
            UpdateView();
        }

        private void OnDestroy()
        {
            _coinService.OnCoinsChanged -= UpdateView;
        }

        private void UpdateView()
        {
            if (_priceText == null) return;
            _priceText.text = _price.ToString();
            _priceText.color = (_coinService != null && _coinService.CoinCount >= _price) ? _affordableColor : _notAffordableColor;
        }
    }
}
