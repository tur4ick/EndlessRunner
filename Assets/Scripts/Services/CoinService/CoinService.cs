using Zenject;

namespace Services.CoinService
{
    public class CoinService
    {
        [Inject] private readonly GameDataService.GameDataService _saveService;

        public int CoinCount => _saveService.Data.Coins;
    
        public void AddCoin(int amount = 1)
        {
            _saveService.Data.Coins += amount;
            _saveService.Save();
        }

        public void Reset()
        {
            _saveService.Data.Coins = 0;
            _saveService.Save();
        }
    }
}
