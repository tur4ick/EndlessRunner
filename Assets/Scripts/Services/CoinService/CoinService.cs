using Zenject;
using System;

namespace Services.CoinService
{
    public class CoinService
    {
        [Inject] private readonly GameDataService.GameDataService _saveService;

        public event Action OnCoinsChanged;

        public int CoinCount { get { return _saveService.Data.Coins; } }

        public void AddCoin(int amount = 1)
        {
            _saveService.Data.Coins = _saveService.Data.Coins + amount;
            _saveService.Save();
            if (OnCoinsChanged != null) OnCoinsChanged.Invoke();
        }

        public bool TrySpend(int amount)
        {
            if (_saveService.Data.Coins >= amount)
            {
                _saveService.Data.Coins = _saveService.Data.Coins - amount;
                _saveService.Save();
                if (OnCoinsChanged != null) OnCoinsChanged.Invoke();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _saveService.Data.Coins = 0;
            _saveService.Save();
            if (OnCoinsChanged != null) OnCoinsChanged.Invoke();
        }
    }
}
