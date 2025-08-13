using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InGameUI : BaseWindow
{
    [SerializeField] private TextMeshProUGUI _coinText;

    private CoinService _coinService;

    [Inject]
    public void Construct(CoinService coinService)
    {
        _coinService = coinService;
    }

    private void Update()
    {
        _coinText.text = $"{_coinService.CoinCount}";
    }
}
