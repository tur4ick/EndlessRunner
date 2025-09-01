using System;
using Services.CoinService;
using Services.WindowService;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace UI.Windows
{
    public class InGameUI : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _distanceText;

        [Inject] private CoinService _coinService;
        [Inject] private DistanceService _distanceService;

        private void OnEnable()
        {
            _distanceService.OnDistanceChanged += UpdateDistanceLine;
            UpdateDistanceLine(_distanceService.SessionDistance);
        }
        private void Update()
        {
            _coinText.text = $"{_coinService.CoinCount}";
        }

        private void OnDisable()
        {
            _distanceService.OnDistanceChanged -= UpdateDistanceLine;
        }

        private void UpdateDistanceLine(float x)
        {
            _distanceText.text = $"{(int)_distanceService.SessionDistance}/{(int)_distanceService.BestDistance}";
        }
    }
}
